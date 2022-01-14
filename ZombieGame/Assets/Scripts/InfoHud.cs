using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using Global;
using UnityEngine;
using UnityEngine.UI;

public class InfoHud : MonoBehaviour
{
    private Player player;
    public int playerNo;

    public Text healthText;
    public Text moneyText;
    public Text ammoText;
    public GameObject hudHolder;
    public GameObject infIcon;

    private void Awake()
    {
        hudHolder.SetActive(false);
    }

    private void Update()
    {
        //Only consider updating if this player is playing the game
        if (GlobalData.Players.Count > playerNo)
        {
            //Haven't saved the player object locally yet, so do that
            if (player == null)
            {
                player = GlobalData.Players[playerNo];
                //Also let's enable the HUD for this player now
                hudHolder.SetActive(true);
                //Also make sure we are showing infIcon and ammoText is empty
                infIcon.SetActive(true);
                ammoText.text = "";
            }
            healthText.text = player.GetHealthString();
            
            //Update money text
            moneyText.text = player.Money.ToString();
            
            //If gun has limited ammo, make sure the ammo holder is active and you update the ammo text
            Gun gun = player.GetGun();
        
            //Gun has limited ammo
            if (gun.HasLimitedAmmo)
            {
                //Need to show the gun ammo and hide the infinity sprite if we are currently showing the infinity icon
                if (infIcon.activeSelf)
                {
                    infIcon.SetActive(false);
                }
                ammoText.text = gun.RemainingAmmo.ToString();
            }
            //Gun doesn't have limited ammo, no need for ammo holder
            else
            {
                if (!infIcon.activeSelf)
                {
                    infIcon.SetActive(true);
                    ammoText.text = "";
                }
            }
        }
    }
}
