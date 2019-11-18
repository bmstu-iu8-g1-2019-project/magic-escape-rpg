using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{

    [Header("Inventory information")]
    public PlayerInventory playerInventory;
    [SerializeField] private GameObject BlankInventorySlot;
    [SerializeField] private GameObject InventoryPanel;

    void Start()
    {
        ClearInventory();
        MakeInventorySlot();
    }

    public void MakeInventorySlot()
    {
        if (playerInventory)
        {
            for (int i = 0; i < playerInventory.MyInventory.Count; i++)
            {
                AddItem(playerInventory.MyInventory[i]); 
            }
        }
    }
    public void ClearInventory()
    {
        foreach (Transform child in InventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void AddItem(InventoryItem item)
    {
        GameObject temp =
                    Instantiate(BlankInventorySlot,
                    InventoryPanel.transform.position, Quaternion.identity);
        temp.transform.SetParent(InventoryPanel.transform);
        temp.transform.localScale = new Vector3(1, 1, 1);
        InventorySlot newSlot = temp.GetComponent<InventorySlot>();
        if (newSlot)
        {
            newSlot.Setup(item, this);
        }
    }

}
