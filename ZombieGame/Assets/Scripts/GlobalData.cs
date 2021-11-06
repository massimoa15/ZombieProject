using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public static class GlobalData
{
    public static List<Player> Players = new List<Player>();
    public static List<GameObject> PlayerObjects = new List<GameObject>();

    private static int WaveNum = 1;

    public static bool IsWaveActive = false;
    
    /// <summary>
    /// Determines if the player exists in this game or not
    /// </summary>
    /// <returns>True if there is a player existing in the game, false otherwise</returns>
    public static bool DoesPlayerExist()
    {
        if (Players == null)
        {
            return false;
        }
        if (PlayerObjects == null)
        {
            return false;
        }
        
        //Still here, player exists
        return true;
    }

    /// <summary>
    /// Increment the wave number. If a value is provided as a parameter then it will increase by that amount, otherwise it will increase by 1
    /// </summary>
    /// <param name="val">Amount to increase by. Defaults to 1 if a value is not provided</param>
    public static void IncrementWaveNum(int val = 1)
    {
        WaveNum += val;
    }

    /// <summary>
    /// Gives the current wave number, an int
    /// </summary>
    /// <returns>Current wave number</returns>
    public static int GetWaveNum()
    {
        return WaveNum;
    }
}
