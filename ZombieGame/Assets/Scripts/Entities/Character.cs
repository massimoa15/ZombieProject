using System;
using UnityEngine;

namespace Entities
{
    public enum Enemy
    {
        Basic
    };
    
    /// <summary>
    /// A character is an entity in the game. Does not extend MonoBehaviour
    /// </summary>
    public class Character
    {
        //Accessor methods  
        public int MaxHealth { get; set; }

        public int CurHealth { get; set; }

        public float Speed { get; set; }
        
        /// <summary>
        /// Damage dealt on contact (perhaps melee implementation for player?)
        /// </summary>
        public int ContactDamage { get; set; }
        
        /// <summary>
        /// Amount of money that this character has. When an enemy character dies, their money goes to the character that killed them.
        /// </summary>
        public int Money { get; private set; }

        /// <summary>
        /// Instantiates character with 3 health, 1 speed, 1 contactDamage, and 1 money
        /// </summary>
        public Character() : this(3, 3, 1, 1)
        {
            
        }
        
        /// <summary>
        /// Manually set all attributes
        /// </summary>
        /// <param name="maxHealth"></param>
        /// <param name="speed"></param>
        /// <param name="contactDamage"></param>
        /// <param name="money"></param>
        public Character(int maxHealth, float speed, int contactDamage, int money)
        {
            MaxHealth = maxHealth;
            CurHealth = maxHealth;
            Speed = speed;
            ContactDamage = contactDamage;
            Money = money;
        }

        /// <summary>
        /// Instantiate character based on the provided wave number.
        /// Needed for enemies which will scale as the player clears waves
        /// </summary>
        /// <param name="wave"></param>
        public Character(int wave)
        {
            Debug.LogWarning("Temporarily basing character off the following stats instead of considering the wave number: 1 health, 0.5 speed, 1 contactDamage, 1 money");
            CurHealth = MaxHealth = 1;
            Speed = 0.5f;
            ContactDamage = 1;
            Money = 1;
        }

        /// <summary>
        /// Instantiate character based on the enemy type provided
        /// </summary>
        /// <param name="enemy">The enemy that we are basing this character on</param>
        public Character(Enemy enemy)
        {
            if (enemy == Enemy.Basic)
            {
                //Basic enemy will be common, easy to kill
                CurHealth = MaxHealth = 50; //This value should be equal to the damage that the default pistol deals (Maybe it should be 2x pistol damage? Maybe it should start lower and scale based on the wave number?)
                Speed = 0.5f;
                ContactDamage = 1;
                Money = 1;
            }
        }

        //Decrease health by the provided amount
        public void TakeDamage(int val)
        {
            CurHealth -= val;
        
            if (CurHealth <= 0)  //Character died
            {
                //Can't have less than 0 health so set to 0
                CurHealth = 0;
                
                //Die
                Die();
            }
        }

        public void Heal(int val)
        {
            CurHealth += val;

            if (CurHealth > MaxHealth)
            {
                CurHealth = MaxHealth;
            }
        }

        public void Heal()
        {
            CurHealth = MaxHealth;
        }

        //Called when the character is dying
        public void Die()
        {
            
        }

        public String GetHealthString()
        {
            return "" + CurHealth + "/" + MaxHealth;
        }

        /// <summary>
        /// Add the provided amount of money to the Money attribute
        /// </summary>
        /// <param name="amt">Amount of money to increase by</param>
        public void AddMoney(int amt)
        {
            Money += amt;
        }
        
        public void RemoveMoney(int amt)
        {
            Money -= amt;
            if (Money < 0)
            {
                Money = 0;
            }
        }
    }
}
