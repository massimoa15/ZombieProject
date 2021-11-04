using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private int cost;

    /// <summary>
    /// Available for purchase
    /// </summary>
    //private bool isAvailable;

    public string name = "";

    public Sprite sprite;

    public Item(int cost)
    {
        this.cost = cost;
        //isAvailable = true;
        name = "";
    }

    public Item(int cost, string name)
    {
        this.cost = cost;
        //this.isAvailable = isAvailable;
        this.name = name;
    }

    public int GetCost()
    {
        return cost;
    }
/*
    public bool GetAvailability()
    {
        return isAvailable;
    }

    public void SetAvailability(bool b)
    {
        isAvailable = b;
    }
*/
    public override string ToString()
    {
        //return "$" + cost + ". Avail: " + isAvailable;
        return "$" + cost + ".";
    }
}
