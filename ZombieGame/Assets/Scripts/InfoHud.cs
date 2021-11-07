using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using UnityEngine.UI;

public class InfoHud : MonoBehaviour
{
    private Player player;
    public int playerNo;

    public Text healthText;

    public Text ammoText;
    public GameObject ammoHolder;
    
    private void Update()
    {
        if (GlobalData.Players.Count > playerNo)
        {
            if (player == null)
            {
                player = GlobalData.Players[playerNo];
            }
            healthText.text = player.GetHealthString();
            //If gun has limited ammo, make sure the ammo holder is active and you update the ammo text
            Gun gun = player.GetGun();
        
            //Gun has limited ammo
            if (gun.HasLimitedAmmo)
            {
                //If the holder is inactive, activate it
                if (!ammoHolder.activeSelf)
                {
                    ammoHolder.SetActive(true);
                }
                ammoText.text = gun.RemainingAmmo.ToString();
            }
            //Gun doesn't have limited ammo, no need for ammo holder
            else
            {
                if (ammoHolder.activeSelf)
                {
                    ammoHolder.SetActive(false);
                }
            }
        }
    }
}
