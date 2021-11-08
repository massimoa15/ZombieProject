using Global;
using UnityEngine;

namespace Entities
{
    public class Player : Character
    {
        /// <summary>
        /// The gun that this player has
        /// </summary>
        private Gun Gun { get; set; }
        
        /// <summary>
        /// How much to decrease the firing delay for this player's guns
        /// </summary>
        public float FiringDelayModifier { get; private set; }
        /// <summary>
        /// How much to increase the damage of this player's bullets on their guns
        /// </summary>
        public int DamageModifier { get; private set; }

        /// <summary>
        /// Assigns the default gun and sets the modifier attributes to 0
        /// </summary>
        public Player()
        {
            AssignDefaultGun();
            //Default modifier values
            FiringDelayModifier = 0;
            DamageModifier = 0;
        }

        /// <summary>
        /// Give the player an upgrade, depending on what name (upgrade type) is passed
        /// </summary>
        /// <param name="name">Name of upgrade</param>
        public void GiveUpgrade(UpgradeName name)
        {
            Debug.Log("Gave upgrade: " + name);
            if (name == UpgradeName.Heal1)
            {
                Heal(1);
            }
            else if (name == UpgradeName.HealthUp)
            {
                MaxHealth += 1;
                CurHealth += 1;
            }
            else if (name == UpgradeName.SpeedUp)
            {
                Speed += 1;
            }
            else if (name == UpgradeName.DamageUp)
            {
                DamageModifier += 5;
            }
            else if (name == UpgradeName.FiringDelayDown)
            {
                //Decrease the firing delay of all guns by this amount below, then apply it to the current gun
                FiringDelayModifier -= 0.01f;
            }
            else if (name == UpgradeName.Rifle)
            {
                //Need to give the player a rifle
                Gun = new Gun(GunType.Rifle);
            }
            else if (name == UpgradeName.SMG)
            {
                //Give player an SMG
                Gun = new Gun(GunType.SMG);
            }
            else
            {
                Debug.LogError("Invalid upgrade name provided " + name + ". No upgrade given");
            }
        }

        /// <summary>
        /// Called when the player wants to shoot their gun, it will call the gun's shoot method and will assign the default gun to this player if their gun is out of ammo
        /// </summary>
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

        /// <summary>
        /// Set this player's gun to the default. Currently it is the infinite ammo pistol
        /// </summary>
        public void AssignDefaultGun()
        {
            //Default gun is the pistol
            Gun = new Gun(GunType.Pistol);
        }

        /// <summary>
        /// Get the firing delay for this player's gun, factoring in their upgrade modified attribute
        /// </summary>
        /// <returns>Accurate firing delay (in seconds)</returns>
        public float GetAccurateFiringDelay()
        {
            float temp = Gun.FiringDelay - FiringDelayModifier;
            if (temp < 0)
            {
                temp = 0;
            }

            return temp;
        }

        /// <summary>
        /// Get the damage for this player's gun, factoring in their upgrade modified attribute
        /// </summary>
        /// <returns>Accurate damage</returns>
        public int GetAccurateDamage()
        {
            return Gun.Damage + DamageModifier;
        }
    }
}
