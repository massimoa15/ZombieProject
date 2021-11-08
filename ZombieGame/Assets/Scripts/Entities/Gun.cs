using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities
{
    public enum GunType
    {
        Pistol,
        Rifle,
        SMG
    };
    /// <summary>
    /// The main root class that will be used by each different gun in the game
    /// </summary>
    public class Gun
    {
        //Accessor methods
    
        /// <summary>
        /// The amount of time that must pass between each bullet being fired
        /// </summary>
        public float FiringDelay { get; set; }
    
        /// <summary>
        /// The amount of ammo left in this gun
        /// </summary>
        public int RemainingAmmo { get; private set; }
        
        /// <summary>
        /// The amount of ammo that this gun starts with
        /// </summary>
        public int StartingAmmo { get; }
    
        /// <summary>
        /// How much damage each bullet will deal
        /// </summary>
        public int Damage { get; set; }

        /// <summary>
        /// True if this gun can run out of ammo, false otherwise
        /// </summary>
        public bool HasLimitedAmmo { get; private set; }
    
        /// <summary>
        /// How many degrees this gun can be inaccurate from in either directions
        /// </summary>
        public float InaccuracyDeg { get; private set; }

        /// <summary>
        /// What type of gun this is
        /// </summary>
        public GunType GunType { get; }

        //Constructors
        public Gun() : this(GunType.Pistol)
        {
            //Default gun is the starting pistol with infinite ammo
        }

        /// <summary>
        /// This constructor will take a gun type and give the appropriate stats for such
        /// </summary>
        /// <param name="gunType">Enum for which gun this is</param>
        public Gun (GunType gunType)
        {
            //Save this gun type locally
            GunType = gunType;
            
            if (gunType == GunType.Pistol)
            {
                HasLimitedAmmo = false;
                Damage = 50;
                StartingAmmo = RemainingAmmo = 1;
                FiringDelay = 0.5f;
                InaccuracyDeg = 1;
            }
            else if (gunType == GunType.Rifle)
            {
                HasLimitedAmmo = true;
                Damage = 75;
                StartingAmmo = RemainingAmmo = 100;
                FiringDelay = 0.2f;
                InaccuracyDeg = 5;
            }
            else if (gunType == GunType.SMG)
            {
                HasLimitedAmmo = true;
                Damage = 30;
                StartingAmmo = RemainingAmmo = 50;
                FiringDelay = 0.1f;
                InaccuracyDeg = 10;
            }
        }

        /// <summary>
        /// Manually set gun parameters. This probably shouldn't be used.
        /// </summary>
        /// <param name="hasLimitedAmmo">True if this gun can run out of ammo, false otherwise</param>
        /// <param name="damage">Amount of base damage the bullets from this gun will do</param>
        /// <param name="ammo">How much ammo this gun has</param>
        /// <param name="firingDelay">Amount of time in seconds between bullets being fired out of this gun</param>
        /// <param name="inaccuracyDeg">How many degrees the bullets from this gun can be inaccurate by</param>
        public Gun(bool hasLimitedAmmo, int damage, int ammo, float firingDelay, float inaccuracyDeg)
        {
            Debug.LogWarning("Should you be using this or should you be using an enum with the other constructor?");
            HasLimitedAmmo = hasLimitedAmmo;
            Damage = damage;
            StartingAmmo = RemainingAmmo = ammo;
            FiringDelay = firingDelay;
            InaccuracyDeg = inaccuracyDeg;
        }

    
        /// <summary>
        /// Called when the shoot gun button is pressed
        /// Will reduce ammo by 1 if it has limitedAmmo = true
        /// </summary>
        /// <returns>Amount of ammo left in gun after it is fired. Will be used to determine if the gun is out of ammo yet</returns>
        public int Shoot()
        {
            if (HasLimitedAmmo)
            {
                RemainingAmmo--;
            }

            return RemainingAmmo;
        }

        public void AddAmmo(int amt)
        {
            RemainingAmmo += amt;
        }

        public void AddAmmo()
        {
            RemainingAmmo += StartingAmmo;
        }
    }
}
