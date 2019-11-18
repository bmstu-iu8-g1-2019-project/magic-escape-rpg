using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadActions : MonoBehaviour
{
    private PlayerManager Player;
    [Header("Scriptable objects")]
    [SerializeField] private PlayerInventory Inv;
    [SerializeField] private PlayerInventory Items;
    [SerializeField] private PlayerEquipment Equipment;
    [Space]
    [SerializeField] private InventoryManager invMgr;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(Player, Inv, Equipment);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPLayer();
        if (!Player)
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        }
        Player.Coins = data.Coins;
        Player.CurrentHealth.RuntimeValue = data.CurrentHealth;
        Player.CurrentHealth.InitialValue = data.MaxHealth;
        Equipment.Armor = new ArmorItem();
        Equipment.Armor = (ArmorItem)Items.MyInventory[data.armorId];
        Player.UpdateArmor.Raise();
        Player.PlayerHealthSignal.Raise();
        Inv.MyInventory.Clear();
        invMgr.ClearInventory();
        for (int i = 0; i < data.itemsId.Count; i++)
        {
            InventoryItem item = Items.MyInventory[data.itemsId[i]];
            Inv.MyInventory.Add(item);
            invMgr.AddItem(item);
        }
        Player.Weapons.thisList.Clear();
        Player.SetWeaponAlpha(0);
        for (int i = 0; i < data.weaponsId.Count; i++)
        {
            Player.SetWeaponAlpha(1);
            Rigidbody2D temp = Items.MyInventory[data.weaponsId[i]].ThisItem.GetComponent<Rigidbody2D>();
            Player.Weapons.thisList.Add(temp);
            Player.ChangeCurrentItem();
        }
    }
}
