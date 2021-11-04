using System;
using Entities;
using UnityEngine;

namespace Environment
{
    /// <summary>
    /// Attaches to an item Game Object. Players can buy this item if they walk into them
    /// </summary>
    public class ItemTriggerPurchase : MonoBehaviour
    {
        public int index;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            //Player triggered this
            if (other.CompareTag("Player"))
            {
                //Get MBPlayer from the collided object
                MBPlayer player = other.GetComponent<MBPlayer>();

                //Check if this player has the money to buy the object
                if (player.GetMoney() >= GlobalShop.Items[index].GetCost())
                {
                    BuyItem(player);
                }
            }
        }

        private void BuyItem(MBPlayer player)
        {
            //Need to take this item out of the shop now
            GlobalShop.BuyItem(index);
            
            //Reduce their money by the cost
            player.RemoveMoney(GlobalShop.Items[index].GetCost());
            
            //Will need to have a "name" string in this class to define what upgrade this is
            player.GiveUpgrade(GlobalShop.Items[index].name);
        }
    }
}
