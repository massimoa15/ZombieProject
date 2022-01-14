using System;
using System.Collections;
using System.Collections.Generic;
using Global;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerJoinController : MonoBehaviour
{
    private void Awake()
    {
        GlobalData.UpdateCurrentPlayerPrefab(1);
    }

    private static int numPlayers = 0;

    /// <summary>
    /// Called when a player joins the game
    /// </summary>
    public static void OnJoin()
    {
        //Someone joined, increment the number of players
        numPlayers++;
        GlobalData.UpdateCurrentPlayerPrefab(numPlayers);
    }
}
