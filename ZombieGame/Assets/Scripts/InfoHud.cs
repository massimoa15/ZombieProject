using System.Collections;
using System.Collections.Generic;
using Entities;
using Environment;
using UnityEngine;
using UnityEngine.UI;

public class InfoHud : MonoBehaviour
{
    public Text text;
    public int playerNo;
    
    void FixedUpdate()
    {
        //Only update the hud if there exists players
        if (GlobalData.Players.Count > 0)
        {
            Player player = GlobalData.Players[playerNo];
            string statText = "\nSpeed: " + player.Speed + ". Damage: " + player.Gun.Damage + ". Firing Delay: " + player.Gun.FiringDelay;
            text.text = "Player health: " + player.GetHealthString() + ". Money: " + player.Money + "\nWave: " + GlobalData.GetWaveNum() + ". Num Rem: " + EnemySpawnerInteractable.GetNumRemEnemies() + statText;
        }
    }
}
