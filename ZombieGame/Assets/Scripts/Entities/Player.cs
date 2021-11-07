using Global;
using UnityEngine;

namespace Entities
{
    public class Player : Character
    {
        //Player's current gun
        private Gun Gun { get; set; }

        public Player()
        {
            AssignDefaultGun();
        }

        /// <summary>
        /// Give the player an upgrade, depending on what name (upgrade type) is passed
        /// </summary>
        /// <param name="name">Name of upgrade</param>
        public void GiveUpgrade(ItemName name)
        {
            Debug.Log("Gave upgrade: " + name);
            if (name == ItemName.Heal1)
            {
                Heal(1);
            }
            else if (name == ItemName.HealthUp)
            {
                MaxHealth += 1;
                CurHealth += 1;
            }
            else if (name == ItemName.SpeedUp)
            {
                Speed += 1;
            }
            else if (name == ItemName.DamageUp)
            {
                Gun.Damage += 1;
            }
            else if (name == ItemName.FiringDelayDown)
            {
                Debug.LogWarning("This should be reworked so that the player has a float value in its object that defines how much it's delay should be changed on all guns");
                float tempDelay = Gun.FiringDelay;
                tempDelay -= 0.05f;
                if (tempDelay < 0)
                {
                    tempDelay = 0;
                }

                Gun.FiringDelay = tempDelay;
            }
            else if (name == ItemName.Rifle)
            {
                //Need to give the player a rifle
                Gun = new Gun(true, 1, 100, 0.2f, 5);
            }
            else if (name == ItemName.SMG)
            {
                //Give player an SMG
                Gun = new Gun(true, 1, 50, 0.1f, 10);
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
