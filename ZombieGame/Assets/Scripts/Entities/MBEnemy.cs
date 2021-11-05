using Environment;
using UnityEngine;

namespace Entities
{
    public class MBEnemy : MonoBehaviour
    {
        private Vector3 directionToPlayer;
        
        void Start()
        {
            //Instantiate character based on the wave number
            Character = new Character(GlobalData.GetWaveNum());
            
            //Should determine the enemy stats based on the player
        }
        
        public Character Character { get; set; }

        private void OnCollisionEnter2D(Collision2D other)
        {
            //If collided with a bullet
            if (other.collider.CompareTag("Bullet"))
            {
                Bullet bullet = other.transform.GetComponent<Bullet>();
                
                //Need to take damage equal to the damage stat from the owner of the bullet
                Character.TakeDamage(bullet.GetDamage());
                bullet.DestroyBullet();
                
                //Check if this enemy now has no health, aka dead
                if (Character.CurHealth <= 0)
                {
                    //If the enemy died, need to give money to the player that killed it
                    bullet.GiveMoneyToBulletOwner(Character.Money);
                    
                    //Kill the enemy
                    Destroy(gameObject);
                    
                    //Decrement the number of remaining enemies
                    EnemySpawnerInteractable.DecreaseRemEnemies();
                }
            }
        }
    }
}
