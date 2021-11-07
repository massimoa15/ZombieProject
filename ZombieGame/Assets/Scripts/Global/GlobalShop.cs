using UnityEditor.Animations;
using Random = System.Random;

namespace Global
{
    public enum ItemName
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
        public static Item[] ItemList = {new Item(5, ItemName.Heal1), new Item(5, ItemName.HealthUp), new Item(5, ItemName.DamageUp), new Item(5, ItemName.SpeedUp), new Item(5, ItemName.FiringDelayDown), new Item(1, ItemName.Rifle), new Item(1, ItemName.SMG)};
        public static AnimatorController[] Animations;
        
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