﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class InventoryItem : ScriptableObject
{
    public UnityEvent ThisEvent;
    public string ItemName;
    public string ItemDescription;
    public Sprite ItemImage;
    public int NumberHeld;
    public bool Usable;
    public bool Unique;
    public int id;
    public int price;
    public int necessaryLevel;
    public GameObject ThisItem;


    public void Use()
    {
        ThisEvent.Invoke();
    }
}
