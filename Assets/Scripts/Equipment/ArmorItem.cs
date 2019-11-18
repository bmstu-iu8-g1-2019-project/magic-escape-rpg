using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Armor")]
public class ArmorItem : InventoryItem
{
    public float armorValue;
    public void OnUse()
    {
        EquipmentManager mgr = GameObject.FindGameObjectWithTag("Equipment").GetComponent<EquipmentManager>();
        if (mgr)
        {
            mgr.addArmorItem(this);
        }
    }
}
