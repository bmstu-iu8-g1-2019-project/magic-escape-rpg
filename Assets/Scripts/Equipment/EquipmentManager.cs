using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    public PlayerEquipment Equipment;

    [Header("Armor")]
    public Signal UpdateArmor;
    public FloatValue ArmorValue;
    [SerializeField] private Image armorImage;

    private void Start()
    {
        UpdateArmorImage();
    }

    public void UpdateArmorImage()
    {
        if (armorImage && Equipment.Armor)
        {
            ArmorValue.InitialValue = Equipment.Armor.armorValue;
            armorImage.sprite = Equipment.Armor.ItemImage;
        }
    }

    public void addArmorItem(ArmorItem item)
    {
        Equipment.Armor = item;
        ArmorValue.InitialValue = item.armorValue;
        UpdateArmor.Raise();
    }

}
