using System.Collections;
using System.Collections.Generic;
using Entities;
using Environment;
using UnityEngine;
using UnityEngine.UI;

public class InfoHud : MonoBehaviour
{
    public Text text;
    
    void FixedUpdate()
    {
        if (GlobalData.Player != null)
        {
            Player player = GlobalData.Player;
            string statText = "\nSpeed: " + player.Speed + ". Damage: " + player.Gun.Damage + ". Firing Delay: " + player.Gun.FiringDelay;
            text.text = "Player health: " + GlobalData.Player.GetHealthString() + ". Money: " + GlobalData.Player.Money + "\nWave: " + GlobalData.GetWaveNum() + ". Num Rem: " + EnemySpawnerInteractable.GetNumRemEnemies() + statText;
        }
    }
}
