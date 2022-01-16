using UnityEngine;
using Random = System.Random;

namespace Global
{
    public enum UpgradeName
    {
        Heal1,
        HealthUp,
        DamageUp,
        SpeedUp,
        FiringDelayDown,
        Rifle,
        SMG
    };

    public static class GlobalShop
    {
        public static Item[] Items = {new Item(5), new Item(5), new Item(5), new Item(5)};
        private static bool[] ItemAvailability = { false, false, false, false };

        private static Random random = new Random();

        //A list of all of the different types of items. This will be used when determining what to put in the shop
        public static Item[] ItemList = {new Item(5, UpgradeName.Heal1), new Item(5, UpgradeName.HealthUp), new Item(5, UpgradeName.DamageUp), new Item(5, UpgradeName.SpeedUp), new Item(5, UpgradeName.FiringDelayDown), new Item(10, UpgradeName.Rifle), new Item(10, UpgradeName.SMG)};
        public static RuntimeAnimatorController[] Animations;

        public static void Reset()
        {
            for (int i = 0; i < Items.Length; i++)
            {
                Items[i] = new Item(5);
                ItemAvailability[i] = false;
            }
        }
        
        /// <summary>
        /// Make item i no longer available
        /// </summary>
        /// <param name="i">Index to make unavailable</param>
        public static void BuyItem(int i)
        {
            ItemAvailability[i] = false;
        }

        /// <summary>
        /// Returns the bool value of item i's availability
        /// </summary>
        /// <param name="i">Index of item that we are interested in</param>
        /// <returns>True if item with index i is available, false otherwise</returns>
        public static bool IsItemAvailable(int i)
        {
            return ItemAvailability[i];
        }

        /// <summary>
        /// Set the availability of an item
        /// </summary>
        /// <param name="i">Index of item to change bool value of</param>
        /// <param name="b">bool value to set</param>
        public static void SetAvailability(int i, bool b)
        {
            ItemAvailability[i] = b;
        }

        public static Item PickRandomItem()
        {
            return ItemList[random.Next(ItemList.Length)];
        }

        /// <summary>
        /// Randomly generates an index between 0 and the length of the list of all the items
        /// </summary>
        /// <returns>Index to take an item from</returns>
        public static int GetRandomItemIndex()
        {
            return random.Next(ItemList.Length);
        }
    }
}