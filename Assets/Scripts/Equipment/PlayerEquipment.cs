using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Equipment")]
public class PlayerEquipment : ScriptableObject
{
    public ArmorItem Armor;
}
