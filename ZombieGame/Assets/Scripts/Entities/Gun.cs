

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
    /// How much damage each bullet will deal
    /// </summary>
    public int Damage { get; set; }

    /// <summary>
    /// True if this gun can run out of ammo, false otherwise
    /// </summary>
    public bool HasLimitedAmmo { get; private set; }

    
    //Constructors
    public Gun() : this(false,1,1,0.5f)
    {
        //Default gun is the starting pistol with infinite ammo
    }

    public Gun(bool hasLimitedAmmo, int damage, int ammo, float firingDelay)
    {
        HasLimitedAmmo = hasLimitedAmmo;
        Damage = damage;
        RemainingAmmo = ammo;
        FiringDelay = firingDelay;
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
}
