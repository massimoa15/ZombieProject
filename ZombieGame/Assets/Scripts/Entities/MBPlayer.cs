using System;
using System.Collections;
using Global;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities
{
    public class MBPlayer : MonoBehaviour
    {
        private Vector2 movementVector;
        private Vector2 shootingVector;
        public GameObject bulletObject;

        private bool waitingToShoot = false;
    
        private BoxCollider2D boxCollider;
        private Rigidbody2D rb;

        private Player player;

        private bool controllerIsDisabled = false;
        
        public bool isInteracting = false;

        private Camera mainCam;

        private Vector2 mousePos;

        private bool holdingShootButton;

        private void Awake()
        {
            //Initialize commonly accessed components
            boxCollider = GetComponent<BoxCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            player = new Player();

            Debug.LogWarning("Manually set player speed to 3 for testing");
            player.Speed = 3;
            
            //Save references in GlobalData
            GlobalData.Players.Add(player);
            GlobalData.PlayerObjects.Add(gameObject);
            
            mainCam = Camera.main;
        }

        private void FixedUpdate()
        {
            //Move according to the vector that is received from the movement input action
            rb.MovePosition(rb.position + movementVector * (player.Speed * Time.deltaTime));
        }

        private void LateUpdate()
        {
            //If the player is shooting (input is not 0), the player is holding a shoot button (left click on mouse or using the right stick on a controller), and the delay has passed, shoot the gun
            if (shootingVector != Vector2.zero && holdingShootButton && !waitingToShoot)
            {
                StartCoroutine(ShootThenWaitCoroutine(player.GetGun().FiringDelay));
            }
        }

        //This will take the input from the user's joystick and save the direction as a vector for movement
        /* THIS IS USED BY THE INPUT SYSTEM. This is how it works:
         * Create the action through the InputActions Player Controls, assigning an input to that action
         * Connect this action to a player input via the Player Input component on the related game object
         * In the Player Input component, add the gameplay event for this action and connect it through the player object's script to the related function 
         */
        public void SaveMovementDirection(InputAction.CallbackContext context)
        {
            if (!controllerIsDisabled && context.performed && player != null)
            {
                //Must normalize the vector. This will make the controller input analog like the keyboard input.
                //movementVector = (context.ReadValue<Vector2>().normalized * player.Speed) * Time.deltaTime;
                movementVector = context.ReadValue<Vector2>().normalized;
            }
            else
            {
                movementVector = Vector2.zero;
            }
        }

        //This will take the input from the user's joystick and save the direction as a vector for movement
        /* THIS IS USED BY THE INPUT SYSTEM. This is how it works:
         * Create the action through the InputActions Player Controls, assigning an input to that action
         * Connect this action to a player input via the Player Input component on the related game object
         * In the Player Input component, add the gameplay event for this action and connect it through the player object's script to the related function 
         */
        public void SaveShootDirection(InputAction.CallbackContext context)
        {
            //If using mouse
            if (context.control.device.description.deviceClass.Equals("Mouse"))
            {
                //If player is clicking their mouse
                if (context.performed)
                {
                    //Set this to true to show that the player is firing their gun
                    holdingShootButton = true;
                }
                else
                {
                    //Set this to false to show that the player is not firing their gun
                    holdingShootButton = false;
                }
            }
            //Not keyboard and mouse so it must be a controller
            else
            {
                if (context.performed)
                {
                    shootingVector = context.ReadValue<Vector2>().normalized;
                    print(shootingVector);
                    holdingShootButton = true;
                }
                else
                {
                    holdingShootButton = false;
                }
            }
        }

        /*
         * Used by the input system, it constantly gets the mouse position and converts it to a shooting vector.
         */
        public void GetMousePosInput(InputAction.CallbackContext context)
        {
            mousePos = context.ReadValue<Vector2>();
            mousePos = mainCam.ScreenToWorldPoint(mousePos);
            shootingVector = ((Vector3) (mousePos) - transform.position).normalized;
        }

        //This will be called when the player hits the interact button. Will check if there is an interactable object within the appropriate radius
        /* THIS IS USED BY THE INPUT SYSTEM. This is how it works:
         * Create the action through the InputActions Player Controls, assigning an input to that action
         * Connect this action to a player input via the Player Input component on the related game object
         * In the Player Input component, add the gameplay event for this action and connect it through the player object's script to the related function 
         */
        public void InputSystemInteract(InputAction.CallbackContext context)
        {
            //If the player is hitting the interact button, save that as a bool
            isInteracting = context.performed;
        }
    
        //This will ensure the user can only shoot when their delay is passed (time between shots)
        IEnumerator ShootThenWaitCoroutine(float delay)
        {
            //Start the waiting process, and store a bool to tell the computer that we are currently waiting
            waitingToShoot = true;
            SummonBullet();
            //"Shoot" the gun, aka reduce ammo if it has limited ammo
            player.ShootGun();
            yield return new WaitForSeconds(delay);
            waitingToShoot = false;
        }

        //Called when a bullet is fired out of the player's gun
        private void SummonBullet()
        {
            Transform _transform = transform;
            //The position of the bullet will be the current position of the player, + the radius of the player in the direction the bullet is being fired
            Vector3 bulletPos = _transform.position + ((Vector3) shootingVector * (boxCollider.size.x * _transform.localScale.x));
        
            //Instantiate bullet and give it a vector
            GameObject bullet = Instantiate(bulletObject, bulletPos, Quaternion.identity);
            bullet.GetComponent<Bullet>().Initialize(shootingVector, player, player.GetGun());
        }

        public void Heal()
        {
            player.Heal();
        }

        public void Heal(int val)
        {
            player.Heal(val);
        }

        //The player has collided with some object
        private void OnCollisionEnter2D(Collision2D other)
        {
            //Collided with an enemy!
            if (other.collider.CompareTag("Enemy"))
            {
                //Take damage equal to the enemy's damage stat
                player.TakeDamage(other.gameObject.GetComponent<MBEnemy>().Character.ContactDamage);
        
                //Do the physics for getting hit by an enemy. Commented out because I don't like how it works right now
                //HitByEnemy();

                //Disable player input momentarily
                //StartCoroutine(DisableControllerCoroutine(0.5f));
                
                //TODO give i-frames
            }
        }

        /// <summary>
        /// Returns the amount of money that the Player object attached to this script has
        /// </summary>
        /// <returns></returns>
        public int GetMoney()
        {
            return player.Money;
        }
        
        /// <summary>
        /// Removes the provided amount of money from the Player object attached to this script
        /// </summary>
        /// <param name="cost"></param>
        public void RemoveMoney(int cost)
        {
            player.RemoveMoney(cost);
        }

        public void GiveUpgrade(ItemName name)
        {
            player.GiveUpgrade(name);
        }
        
    }
}
