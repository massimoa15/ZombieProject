
using System;
using System.Collections.Generic;

public static class GlobalShop
{
    public static Item[] Items = {new Item(5), new Item(5), new Item(5), new Item(5)};
    private static bool[] ItemAvailability = { true, true, true, true };

    private static Random random = new Random();
    

    //A list of all of the different types of items. This will be used when determining what to put in the shop
    //public static Item[] ItemList = {new Item(5, true, "Heal1"), new Item(5, true, "HealthUp"), new Item(5, true, "DamageUp"), new Item(5, true, "SpeedUp"), new Item(5, true, "FiringDelayDown")};
    public static Item[] ItemList = {new Item(5, "Heal1"), new Item(5, "HealthUp"), new Item(5, "DamageUp"), new Item(5, "SpeedUp"), new Item(5, "FiringDelayDown")};
    
    public static void BuyItem(int i)
    {
        //Items[i].SetAvailability(false);
        ItemAvailability[i] = false;
    }

    public static bool IsItemAvailable(int i)
    {
        //return Items[index].GetAvailability();
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
}