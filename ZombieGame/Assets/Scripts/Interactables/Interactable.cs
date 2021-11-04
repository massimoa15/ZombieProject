using Entities;
using UnityEngine;

namespace Interactables
{
    /// <summary>
    /// All classes that inherit Interactable need to implement the abstract class Interact. This class is what will be called when there is a player within the Circle Collider range (activating the trigger), and pressing their interact button
    /// </summary>
    public abstract class Interactable : MonoBehaviour
    {
        public float radius = 2f;               //The acceptable radius to let player's interact with this object
    
        private void Start()
        {
            //Set the circle collider radius equal to the saved radius
            SetInteractRadius(radius);
        }
    
        private void OnTriggerStay2D(Collider2D other)
        {
            //Only do something if it's a player in the interactable zone
            if (other.tag.Equals("Player"))
            {
                //Get MBPlayer script
                MBPlayer player = other.GetComponent<MBPlayer>();
                //Check if player is trying to interact, only if this object is interactable
                if (player.isInteracting)
                {
                    Interact(player);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag.Equals("Player"))
            {
                //print("see you later bro");
            }
        }

        public abstract void Interact(MBPlayer player);

        /// <summary>
        /// Updates saved radius value and sets the collider radius to that value
        /// </summary>
        /// <param name="newRadius"></param>
        public void SetInteractRadius(float newRadius)
        {
            radius = newRadius;
            GetComponent<CircleCollider2D>().radius = radius;
        }
    }
}
