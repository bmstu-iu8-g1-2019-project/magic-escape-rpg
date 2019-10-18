﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler
{
    [Header("UI to change")]
    [SerializeField] private TextMeshProUGUI ItemNumberText;
    [SerializeField] private Image ItemImage;
    private GameObject DescriptionPanel;
    private GameObject ItemDescription;


    [Header("Variables from the Item")]
    public InventoryItem ThisItem;
    public InventoryManager ThisManager;

    private void Start()
    {
        DescriptionPanel = GameObject.Find("Canvas/Inventory Panel/Description Panel");
        ItemDescription = GameObject.Find("Canvas/Inventory Panel/Description Panel/Item Description");
    }
    public void Setup(InventoryItem NewItem, InventoryManager NewManager)
    {
        ThisItem = NewItem;
        ThisManager = NewManager;
        if (ThisItem)
        {
            ItemImage.sprite = ThisItem.ItemImage;
            ItemNumberText.text = "" + ThisItem.NumberHeld; // Convertion int to string
        }
    }
    
    public void OnPointerEnter(PointerEventData eventdata)
    {
        ItemDescription.GetComponent<TextMeshProUGUI>().text = ThisItem.ItemDescription;
    }

    public void OnCLick()
    {
        if (ThisItem)
        {

            Destroy(this.gameObject);
        }
    }
}
