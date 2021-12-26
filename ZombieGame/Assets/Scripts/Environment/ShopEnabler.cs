using Global;
using UnityEngine;

namespace Environment
{
    public class ShopEnabler : MonoBehaviour
    {
        public GameObject ShopUIHolder;

        public GameObject[] textPrices;

        void Update()
        {
            //Need to show the shop when the wave is over, and hide it when it has begun
            if (!GlobalData.IsWaveActive)
            {
                EnableShop();
            }
            else
            {
                if (ShopUIHolder.activeSelf)
                {
                    DisableShop();
                }
            }
        }

        /// <summary>
        /// Show the shop to the player. This also needs to show the player all of the items
        /// </summary>
        private void EnableShop()
        {
            //Enable all child game objects (they all are items)
            for (int i = 0; i < transform.childCount; i++)
            {
                if (GlobalShop.IsItemAvailable(i))
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                    textPrices[i].SetActive(true);
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                    textPrices[i].SetActive(false);
                }
            }
            ShopUIHolder.SetActive(true);
        }

        /// <summary>
        /// Hide the shop from the player
        /// </summary>
        private void DisableShop()
        {
            //Disable everything
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject itemObject = transform.GetChild(i).gameObject;
                //Disable the game object to hide it
                itemObject.SetActive(false);
                
                //Now, change all of the items and set them all to available
                int itemListInd = GlobalShop.GetRandomItemIndex();
                GlobalShop.Items[i] = GlobalShop.ItemList[itemListInd];
                GlobalShop.SetAvailability(i, true);
                
                //Need to assign the animator for this object as well
                itemObject.GetComponent<Animator>().runtimeAnimatorController = GlobalShop.Animations[itemListInd];
                //print("Assigned shop index " + i + " to " + GlobalShop.Items[i].Name);
            }
            ShopUIHolder.SetActive(false);
        }
    }
}
