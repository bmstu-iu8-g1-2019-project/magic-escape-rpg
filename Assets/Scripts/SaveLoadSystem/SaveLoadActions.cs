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
    [SerializeField] private PlayerInventory Shop;
    [Space]
    [SerializeField] private InventoryManager invMgr;
    [SerializeField] private InventoryManager shopMgr;
    [Header("Default items/values")]
    [SerializeField] private ArmorItem defArmor;
    [SerializeField] private PlayerInventory defShop;
    [SerializeField] private PlayerInventory defInv;
    [Header("Signals")]
    public Signal UpdateInv;
    public Signal UpdateShop;
    public Signal UpdateCoins;
    public Signal UpdatePlayerLevel;
 
    public void SavePlayer()
    {
        if (!Player)
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        }
        SaveSystem.SavePlayer(Player, Inv, Equipment, Shop);
    }

    public void LoadPlayer()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        PlayerData data = SaveSystem.LoadPLayer();
        if (!Player)
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        }
        if (data == null)
        {
            ResetDefaults();
        }
        Player.bossesProgres = data.bossesProgress;
        Player.Level = data.Level;
        Player.Stars = data.Stars;
        UpdatePlayerLevel.Raise();
        Player.Coins = data.Coins;
        UpdateCoins.Raise();
        Player.CurrentHealth.RuntimeValue = data.CurrentHealth;
        Player.CurrentHealth.InitialValue = data.MaxHealth;
        Equipment.Armor = new ArmorItem();
        Equipment.Armor = (ArmorItem)Items.MyInventory[data.armorId];
        Player.UpdateArmor.Raise();
        Player.PlayerHealthSignal.Raise();
        if (invMgr)
        {
            Inv.MyInventory.Clear();
            invMgr.ClearInventory();
            for (int i = 0; i < data.itemsId.Count; i++)
            {
                InventoryItem item = Items.MyInventory[data.itemsId[i]];
                item.NumberHeld = data.itemsValue[i];
                Inv.MyInventory.Add(item);
                invMgr.AddItem(item);
            }
        }
        if (shopMgr)
        {
            Shop.MyInventory.Clear();
            shopMgr.ClearInventory();
            for (int i = 0; i < data.shopId.Count; i++)
            {
                InventoryItem item = Items.MyInventory[data.shopId[i]];
                Shop.MyInventory.Add(item);
            }
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
        UpdateInv.Raise();
        UpdateShop.Raise();
    }

    public void ResetDefaults()
    {
        if (!Player)
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        }
        Player.Stars = 0;
        Player.Level = 1;
        Player.Coins = 1000;
        Player.CurrentHealth.InitialValue = 6;
        Player.bossesProgres = 0;
        Player.CurrentHealth.RuntimeValue = Player.CurrentHealth.InitialValue;
        Equipment.Armor = new ArmorItem();
        Equipment.Armor = defArmor;
        Player.UpdateArmor.Raise();
        Shop = defShop;
        UpdateShop.Raise();
        Inv = defInv;
        foreach (var item in Items.MyInventory)
        {
            item.NumberHeld = 1;
        }
        UpdateInv.Raise();
        Player.Weapons.thisList.Clear();
        SavePlayer();
        LoadPlayer();
    }
}
