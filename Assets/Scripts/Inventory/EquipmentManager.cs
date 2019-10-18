using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    public PlayerEquipment Equipment;
    public Image RangeDamage;

    private void Start()
    {
        CheckItem();
    }

    public void CheckItem()
    {
        if (RangeDamage.sprite != Equipment.RangeDamageItem.Image)
        {
            RangeDamage.sprite = Equipment.RangeDamageItem.Image;
        }
    }
}
