using System;
using System.Collections;
using UnityEngine;

namespace Entities
{
    public class Bullet : MonoBehaviour
    {
        private Vector2 direction;  //Direction the bullet will travel
        private float speed = 10;    //Arbitrarily chosen, can be changed if needed

        public float destroyDelay = 2; //Amount of time (in seconds) before this bullet is destroyed

        private Player bulletOwnerCharacter;

        public GameObject bulletObject;

        public Gun gun;
        
        private Rigidbody2D rb;

        /// <summary>
        /// Initialize this bullet with some important information
        /// </summary>
        /// <param name="direction">The vector holding the direction that this bullet will be travelling in</param>
        /// <param name="player">The Player object that fired this bullet</param>
        /// <param name="gun">The gun that this bullet was fired from</param>
        public void Initialize(Vector2 direction, Player player, Gun gun)
        {
            this.direction = direction;
            bulletOwnerCharacter = player;
            this.gun = gun;
            rb = GetComponent<Rigidbody2D>();
        }
        
        private void Start()
        {
            //Wait for destroyDelay seconds and then destroy the bullet
            StartCoroutine(WaitThenDestroyCoroutine(destroyDelay));
        }
        
        private void FixedUpdate()
        {
            //Move according to the vector that the bullet is fired at
            rb.MovePosition(rb.position + direction * (speed * Time.deltaTime));
        }

        IEnumerator WaitThenDestroyCoroutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            DestroyBullet();
        }
        
        public void DestroyBullet()
        {
            Destroy(gameObject);
        }

        public Character GetCharacter()
        {
            return bulletOwnerCharacter;
        }

        /// <summary>
        /// Gets the damage that this bullet does. Comes from the gun that fired it's object
        /// </summary>
        /// <returns>Amount of damage that this bullet will deal as an int</returns>
        public int GetDamage()
        {
            return gun.Damage;
        }

        /// <summary>
        /// Increases the amount of money that the bullet owner has by the provided value
        /// </summary>
        /// <param name="val">Amount of money to give</param>
        public void GiveMoneyToBulletOwner(int val)
        {
            bulletOwnerCharacter.AddMoney(val);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            //Need to destroy the bullet when it collides with a wall
            if (other.collider.CompareTag("Wall"))
            {
                //Destroy bullet
                DestroyBullet();
            }
        }

        /// <summary>
        /// Get the accurate damage for this bullet, factoring in the player's damage attribute
        /// </summary>
        /// <returns>The amount of damage this bullet should deal</returns>
        public int GetAccurateDamage()
        {
            return bulletOwnerCharacter.GetAccurateDamage();
        }
    }
}
