using System;
using System.Collections;
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
    
        private float shootingDelay = 0.5f;

        public bool isInteracting = false;
        
        private void Awake()
        {
            //Initialize commonly accessed components
            boxCollider = GetComponent<BoxCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            player = new Player();

            Debug.LogWarning("Manually set player speed to 3 for testing");
            player.Speed = 3;
            
            //Save references in GlobalData
            GlobalData.Player = player;
            GlobalData.PlayerObject = gameObject;
        }

        private void FixedUpdate()
        {
            //Move according to the vector that is received from the movement input action
            rb.MovePosition(rb.position + movementVector * (player.Speed * Time.deltaTime));
        }

        private void LateUpdate()
        {
            //If the player is shooting (input is not 0), and the delay has passed, shoot the gun
            if (shootingVector != Vector2.zero && !waitingToShoot)
            {
                StartCoroutine(ShootThenWaitCoroutine(shootingDelay));
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
            shootingVector = context.ReadValue<Vector2>().normalized;
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
            SummonBullet();
            //Start the waiting process, and store a bool to tell the computer that we are currently waiting
            waitingToShoot = true;
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
            bullet.GetComponent<Bullet>().Initialize(shootingVector, player, player.Gun);
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

        public void GiveUpgrade(string name)
        {
            player.GiveUpgrade(name);
        }

        public void HitByEnemy()
        {
            //Get bumped backward because the enemy smacked you
            //This isn't working properly
            rb.AddForce(-movementVector.normalized * 5, ForceMode2D.Impulse);
        }

        IEnumerator DisableControllerCoroutine(float delay)
        {
            controllerIsDisabled = true;
            yield return new WaitForSeconds(delay);
            controllerIsDisabled = false;
        }

    }
}
