using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public float CurrentHealth;
    public float MaxHealth;
    public int Stars;
    public int Coins;
    public float Armor;
    public int armorId;
    public int Level;
    public int bossesProgress;
    public List<int> weaponsId = new List<int>();
    public List<int> itemsId = new List<int>();
    public List<int> itemsValue = new List<int>();
    public List<int> shopId = new List<int>();

    public PlayerData(PlayerManager Player, PlayerInventory Inv, PlayerEquipment equipment, PlayerInventory Shop)
    {
        Level = Player.Level;
        CurrentHealth = Player.CurrentHealth.RuntimeValue;
        Stars = Player.Stars;
        MaxHealth = Player.CurrentHealth.InitialValue;
        Armor = Player.Armor.InitialValue;
        armorId = equipment.Armor.id;
        Coins = Player.Coins;
        bossesProgress = Player.bossesProgres;
        foreach (var item in Inv.MyInventory)
        {
            itemsId.Add(item.id);
            itemsValue.Add(item.NumberHeld);
        }
        foreach (var item in Shop.MyInventory)
        {
            shopId.Add(item.id);
        }
        foreach (var item in Player.Weapons.thisList)
        {
            weaponsId.Add(item.gameObject.GetComponent<MagicCast>().ThisItem.id);
        }
    }
}
