using System.Collections;
using System.Collections.Generic;
using Entities;
using Environment;
using Global;
using Interactables;
using UnityEngine;
using UnityEngine.UI;

public class InfoHudDebug : MonoBehaviour
{
    public Text text;
    public int playerNo;
    
    void FixedUpdate()
    {
        //Only update the hud if there exists players
        if (GlobalData.Players.Count > 0)
        {
            Player player = GlobalData.Players[playerNo];
            string statText = "\nSpeed: " + player.Speed + ". Damage: " + player.GetGun().Damage + ". Firing Delay: " + player.GetGun().FiringDelay;
            text.text = "Player health: " + player.GetHealthString() + ". Money: " + player.Money + "\nWave: " + GlobalData.GetWaveNum() + ". Num Rem: " + EnemySpawnerInteractable.GetNumRemEnemies() + statText;
        }
    }
}
