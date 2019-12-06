using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Buff item")]
public class BuffItem : InventoryItem
{
    public BuffParametrs parametrs;

    public void OnUse()
    {
        BuffManager mgr = GameObject.FindGameObjectWithTag("GameController").GetComponent<BuffManager>();
        mgr.Buff(parametrs);
    }
}
