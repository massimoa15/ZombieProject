using Environment;
using Global;
using Interactables;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = System.Random;

namespace Entities
{
    public class MBEnemy : MonoBehaviour
    {
        private Vector3 directionToPlayer;
        public Enemy type;

        private Slider healthBar;
        private GameObject healthBarObj;
        
        void Start()
        {
            //Instantiate character based on the wave number
            Character = new Character(type);
            gameObject.GetComponent<NavMeshAgent>().speed = Character.Speed;
        }
        
        public Character Character { get; set; }

        private void OnCollisionEnter2D(Collision2D other)
        {
            //If collided with a bullet
            if (other.collider.CompareTag("Bullet"))
            {
                Bullet bullet = other.transform.GetComponent<Bullet>();
                
                //Need to take damage equal to the damage stat from the owner of the bullet
                Character.TakeDamage(bullet.GetAccurateDamage());
                bullet.DestroyBullet();
                
                //Now that we took damage, update the slider
                if (!healthBarObj.activeSelf)
                {
                    healthBarObj.SetActive(true);
                }
                healthBar.value = (float) Character.CurHealth / Character.MaxHealth;
                
                //Check if this enemy now has no health, aka dead
                if (Character.CurHealth <= 0)
                {
                    //If the enemy died, need to give money to the player that killed it
                    bullet.GiveMoneyToBulletOwner(Character.Money);
                    
                    GlobalData.IncrementEnemiesKilled();

                    //Destroy the health bar
                    Destroy(healthBarObj);
                    
                    //Kill the enemy
                    Destroy(gameObject);

                    //Decrement the number of remaining enemies
                    EnemySpawnerInteractable.DecreaseRemEnemies();
                }
            }
        }

        public void SetSlider(GameObject sliderObj)
        {
            healthBarObj = sliderObj;
            healthBar = sliderObj.GetComponent<Slider>();
        }
    }
}
