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
    /// Called when player selects a character
    /// </summary>
    public static void OnJoin()
    {
        //Someone joined, increment the number of players
        numPlayers++;
        GlobalData.UpdateCurrentPlayerPrefab(numPlayers);
    }

    /// <summary>
    /// Called when player leaves the character select screen
    /// </summary>
    public void OnLeave(InputAction.CallbackContext callbackContext)
    {
        numPlayers--;
        print("bye");
    }
}
