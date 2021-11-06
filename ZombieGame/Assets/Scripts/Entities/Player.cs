using UnityEngine;

namespace Entities
{
    public class Player : Character
    {
        //Attributes

        //Constructor
        public Player()
        {
            Gun = new Gun();
        }

        //Player's current gun
        private Gun Gun { get; set; }

        /// <summary>
        /// Give the player an upgrade, depending on what name (upgrade type) is passed
        /// </summary>
        /// <param name="name">Name of upgrade</param>
        public void GiveUpgrade(string name)
        {
            Debug.Log("Gave upgrade: " + name);
            if (name.Equals("Heal1"))
            {
                Heal(1);
            }
            else if (name.Equals("HealthUp"))
            {
                MaxHealth += 1;
                CurHealth += 1;
            }
            else if (name.Equals("SpeedUp"))
            {
                Speed += 1;
            }
            else if (name.Equals("DamageUp"))
            {
                Gun.Damage += 1;
            }
            else if (name.Equals("FiringDelayDown"))
            {
                float tempDelay = Gun.FiringDelay;
                tempDelay -= 0.05f;
                if (tempDelay < 0)
                {
                    tempDelay = 0;
                }

                Gun.FiringDelay = tempDelay;
            }
            else if (name.Equals("Rifle"))
            {
                //Need to give the player a 
                Gun = new Gun(true, 1, 30, 0.1f);
            }
            else
            {
                Debug.LogError("Invalid upgrade name provided " + name + ". No upgrade given");
            }
        }

        //This will be called whenever the player wants to shoot their gun
        public void ShootGun()
        {
            //Gun.Shoot returns the new remaining amount of ammo. If the gun has no ammo left, we need to remove this gun
            if (Gun.Shoot() <= 0)
            {
                AssignDefaultGun();
            }
        }

        /// <summary>
        /// Get the player's current gun
        /// </summary>
        /// <returns>The player's current gun</returns>
        public Gun GetGun()
        {
            return Gun;
        }

        public void AssignDefaultGun()
        {
            //This calls the default constructor
            Gun = new Gun();
        }
    }
}
