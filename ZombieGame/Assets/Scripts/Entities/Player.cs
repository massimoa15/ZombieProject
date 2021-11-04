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
        public Gun Gun { get; }

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
            else
            {
                Debug.LogError("Invalid upgrade name provided " + name + ". No upgrade given");
            }
        }
    }
}
