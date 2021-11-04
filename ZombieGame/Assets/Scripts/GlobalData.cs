using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public static class GlobalData
{
    public static Player Player;
    public static GameObject PlayerObject;

    private static int WaveNum = 1;

    public static bool IsWaveActive = false;
    
    /// <summary>
    /// Determines if the player exists in this game or not
    /// </summary>
    /// <returns>True if there is a player existing in the game, false otherwise</returns>
    public static bool DoesPlayerExist()
    {
        if (Player == null)
        {
            return false;
        }
        if (PlayerObject == null)
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

    public static int GetWaveNum()
    {
        return WaveNum;
    }
}
