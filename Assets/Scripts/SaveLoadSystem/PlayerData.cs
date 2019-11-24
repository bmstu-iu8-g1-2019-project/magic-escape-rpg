using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public float CurrentHealth;
    public float MaxHealth;
    public int Coins;
    public float Armor;
    public int armorId;
    public List<int> weaponsId = new List<int>();
    public List<int> itemsId = new List<int>();
    public List<int> shopId = new List<int>();

    public PlayerData(PlayerManager Player, PlayerInventory Inv, PlayerEquipment equipment, PlayerInventory Shop)
    {

        CurrentHealth = Player.CurrentHealth.RuntimeValue;
        MaxHealth = Player.CurrentHealth.InitialValue;
        Armor = Player.Armor.InitialValue;
        armorId = equipment.Armor.id;
        Coins = Player.Coins;
        foreach (var item in Inv.MyInventory)
        {
            itemsId.Add(item.id);
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
