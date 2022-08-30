using System;
using System.Collections;
using Global;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = System.Random;

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
        
        private Random random = new Random();

        private SpriteRenderer _renderer;

        private bool isInvincible = false;
        private float invincibilityDurat = 2f;
        private float flashDelay = 0.1f;

        public static PauseMenu pauseMenu;

        private void Start()
        {
            pauseMenu = GlobalData.pauseMenu;
        }

        private void Awake()
        {
            //Initialize commonly accessed components
            boxCollider = GetComponent<BoxCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            player = new Player();
            
            //Save references in GlobalData
            GlobalData.Players.Add(player);
            GlobalData.PlayerObjects.Add(gameObject);
            
            mainCam = Camera.main;

            _renderer = GetComponent<SpriteRenderer>();
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
                StartCoroutine(ShootThenWaitCoroutine(player.GetAccurateFiringDelay()));
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
                    holdingShootButton = true;
                    if (_renderer != null)
                    {
                        //Update the renderer to flip if the x value of the shooting vector is negative
                        if (shootingVector.x < 0)
                        {
                            _renderer.flipX = true;
                        }
                        else
                        {
                            _renderer.flipX = false;
                        }
                    }
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
            //Set mainCam if it is null
            if (mainCam == null)
            {
                mainCam = Camera.main;
            }
            mousePos = mainCam.ScreenToWorldPoint(mousePos);
            shootingVector = ((Vector3) (mousePos) - transform.position).normalized;

            if (_renderer != null)
            {
                //Check if the mousePos is less than the character x position. If it is, make the character face the left by flipping the sprite
                if (mousePos.x < transform.position.x)
                {
                    _renderer.flipX = true;
                }
                else
                {
                    _renderer.flipX = false;
                }
            }
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

        /// <summary>
        /// Called when a bullet is fired out of the player's gun. Summons a bullet in the firing direction the player provided and instantiates the bullet with the appropriate values
        /// </summary>
        private void SummonBullet()
        {
            Transform _transform = transform;

            //Make the shooting vector inaccurate based on the gun's inaccuracy
            shootingVector = DetermineInaccurateShootingVector(shootingVector);
            
            //The position of the bullet will be the current position of the player, + the radius of the player in the direction the bullet is being fired
            Vector3 bulletPos = _transform.position + ((Vector3) shootingVector * (boxCollider.size.x * _transform.localScale.x));
        
            //Instantiate bullet and give it a vector
            GameObject bullet = Instantiate(bulletObject, bulletPos, Quaternion.identity);
            bullet.GetComponent<Bullet>().Initialize(shootingVector, player, player.GetGun());
        }

        /// <summary>
        /// Calls Player.Heal(). Heals 1 
        /// </summary>
        public void Heal()
        {
            player.Heal();
        }

        /// <summary>
        /// Calls Player.Heal(val)
        /// </summary>
        /// <param name="val">Amount to heal</param>
        public void Heal(int val)
        {
            player.Heal(val);
        }

        //The player has collided with some object
        private void OnCollisionEnter2D(Collision2D other)
        {
            //Collided with an enemy!
            if (other.collider.CompareTag("Enemy") && !isInvincible)
            {
                //Take damage equal to the enemy's damage stat
                var remHealth = player.TakeDamage(other.gameObject.GetComponent<MBEnemy>().Character.ContactDamage);

                //Player died
                if (remHealth <= 0)
                {
                    GlobalData.Players.Remove(player);
                    //Nobody is still alive in this game
                    if (GlobalData.Players.Count <= 0)
                    {
                        SceneManager.LoadScene(2);
                    }
                    Destroy(this);
                }
                
                //Give I-frames
                StartCoroutine(GiveInvincibilityCoroutine(invincibilityDurat, flashDelay));
            }
        }

        /// <summary>
        /// Make the player invincible for the provided amount of time
        /// </summary>
        /// <param name="totalDelay">Amount of time to be invincible for</param>
        /// <returns></returns>
        IEnumerator GiveInvincibilityCoroutine(float totalDelay, float flashDelay)
        {
            isInvincible = true;
            float amtWaited = 0f;
            while (amtWaited < totalDelay)
            {
                _renderer.enabled = !_renderer.enabled;
                yield return new WaitForSeconds(flashDelay);
                amtWaited += flashDelay;
            }

            _renderer.enabled = true;
            isInvincible = false;
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

        /// <summary>
        /// Give the player an upgrade based on the UpgradeName they provided
        /// </summary>
        /// <param name="name">Name of the upgrade being given</param>
        public void GiveUpgrade(UpgradeName name)
        {
            player.GiveUpgrade(name);
        }

        /// <summary>
        /// Takes the accurate shooting vector provided by the user and returns a new, inaccurate vector based on the guns innacuracy
        /// </summary>
        /// <param name="origVec">Vector that the player is trying to shoot at</param>
        /// <returns>Vector that the bullet will be shot at</returns>
        private Vector2 DetermineInaccurateShootingVector(Vector2 origVec)
        {
            //Need to make the shooting vector inaccurate based on the gun's inaccuracy
            Gun playerGun = player.GetGun();
            float gunInacc = playerGun.InaccuracyDeg;
            float origTheta = Mathf.Rad2Deg * Mathf.Atan((origVec.x / origVec.y));

            float maxValue = origTheta + gunInacc;
            float minValue = origTheta - gunInacc;

            float newTheta = (float)(random.NextDouble() * (maxValue - minValue) + minValue);
            
            //We can use this new angle mixed with hypotenuse = 1 (normalized vector) to determine the x and y components of the new inaccurate bullet. Below are the trigonometric conversions since we have hyp = 1
            float x = Mathf.Abs(Mathf.Sin(Mathf.Deg2Rad * newTheta));
            float y = Mathf.Abs(Mathf.Cos(Mathf.Deg2Rad * newTheta));
            
            //Now change sign of x and y if necessary
            if (shootingVector.x < 0)
            {
                x *= -1;
            }

            if (shootingVector.y < 0)
            {
                y *= -1;
            }

            //Save values in shooting vector
            return new Vector2(x, y);
        }

        public void TogglePause(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                pauseMenu.TogglePause();
            }
        }

        public string GetStatString()
        {
            return player.GetStatString();
        }
    }
}
