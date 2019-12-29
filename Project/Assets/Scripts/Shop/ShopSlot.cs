using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ShopSlot : InventorySlot
{
    [SerializeField] private PlayerInventory Shop;
    [SerializeField] private Signal UpdateInventory;
    [SerializeField] private Signal UpdateCoins;
    [SerializeField] private TextMeshProUGUI Price;

    override public void Start()
    {
        ItemDescription = GameObject.Find("UI Canvas/Shop Panel/Description Panel/Item Description");
    }

    public override void SetupText()
    {
        Price.text = "" + ThisItem.price;
    }

    public override void OnPointerEnter(PointerEventData eventdata)
    {
        if (!ItemDescription)
        {
            ItemDescription = GameObject.FindGameObjectWithTag("Description");
        }
        ItemDescription.GetComponent<TextMeshProUGUI>().text = ThisItem.ItemDescription;
        PlayerManager player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        if (player.Level < ThisItem.necessaryLevel)
        {
            ItemDescription.GetComponent<TextMeshProUGUI>().text += "\nThis item will be unlocked on the " + ThisItem.necessaryLevel + " level";
        }
    }

    public override void OnCLick()
    {
        if (ThisItem)
        {
            PlayerManager player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
            Debug.Log(player.Coins);
            if (player.Coins - ThisItem.price >= 0)
            {
                player.Coins -= ThisItem.price;
                UpdateCoins.Raise();
                AddItem();
                UpdateInventory.Raise();
                ItemDescription.GetComponent<TextMeshProUGUI>().text = "";
                if (ThisItem.Unique)
                {
                    Destroy(this.gameObject);
                    for (int i = 0; i < Shop.MyInventory.Count; i++)
                    {
                        if (ThisItem.name == Shop.MyInventory[i].name)
                        {
                            Shop.MyInventory.Remove(Shop.MyInventory[i]);
                        }
                    }
                }
            }
        }
    }

    private void AddItem()
    {
        foreach (var item in Inventory.MyInventory)
        {
            if (item == ThisItem)
            {
                item.NumberHeld++;
                return;
            }
        }
        ThisItem.NumberHeld = 1;
        Inventory.MyInventory.Add(ThisItem);
    }

}
