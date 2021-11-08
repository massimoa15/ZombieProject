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
        public static Item[] ItemList = {new Item(5, UpgradeName.Heal1), new Item(5, UpgradeName.HealthUp), new Item(5, UpgradeName.DamageUp), new Item(5, UpgradeName.SpeedUp), new Item(5, UpgradeName.FiringDelayDown), new Item(1, UpgradeName.Rifle), new Item(1, UpgradeName.SMG)};
        public static RuntimeAnimatorController[] Animations;
        
        public static void BuyItem(int i)
        {
            ItemAvailability[i] = false;
        }

        public static bool IsItemAvailable(int i)
        {
            return ItemAvailability[i];
        }

        public static void SetAvailability(int i, bool b)
        {
            ItemAvailability[i] = b;
        }

        public static Item PickRandomItem()
        {
            return ItemList[random.Next(ItemList.Length)];
        }

        public static int GetRandomItemIndex()
        {
            return random.Next(ItemList.Length);
        }
    }
}