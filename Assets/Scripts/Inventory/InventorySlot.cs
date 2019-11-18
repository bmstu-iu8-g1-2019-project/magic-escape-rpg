using System.Collections;
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
    private GameObject ItemDescription;


    [Header("Variables from the Item")]
    public InventoryItem ThisItem;
    public InventoryManager ThisManager;

    [Space]
    public PlayerInventory Inventory;

    private void Start()
    {
        ItemDescription = GameObject.Find("UI Canvas/Inventory Panel/Description Panel/Item Description");
    }
    public void Setup(InventoryItem NewItem, InventoryManager NewManager)
    {
        ThisItem = NewItem;
        ThisManager = NewManager;
        if (ThisItem)
        {
            ItemImage.sprite = ThisItem.ItemImage;
            if (!ThisItem.Unique)
            {
                ItemNumberText.text = "" + ThisItem.NumberHeld; // Convertion int to string
            }
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
            ThisItem.Use();
            ItemDescription.GetComponent<TextMeshProUGUI>().text = "";
            if (ThisItem.NumberHeld == 1 || ThisItem.Unique)
            {
                Destroy(this.gameObject);
                for (int i = 0; i < Inventory.MyInventory.Count; i++)
                {
                    if (ThisItem.name == Inventory.MyInventory[i].name)
                    {
                        Inventory.MyInventory.Remove(Inventory.MyInventory[i]);
                    }
                }
            }
            else
            {
                ThisItem.NumberHeld--;
                ItemNumberText.text = "" + ThisItem.NumberHeld;
            }
        }
    }
}
