﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Weapon")]
public class WeaponItem : InventoryItem
{
    public GameObject ThisItem;
    public DefaultKnock KnockParams;

    public void OnUse()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if (Player)
        {
            Player.GetComponent<PlayerManager>().Weapons.Add(ThisItem.GetComponent<Rigidbody2D>());
            Player.GetComponent<PlayerManager>().ChangeCurrentItem();  
        }
    }
}
