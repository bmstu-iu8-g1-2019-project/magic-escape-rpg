using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public float CurrentHealth;
    public float MaxHealth;
    public float[] position;
    public int[] weaponsId;
    public List<int> itemsId = new List<int>();

    public PlayerData(PlayerManager Player, PlayerInventory Inv)
    {
        CurrentHealth = Player.CurrentHealth.RuntimeValue;
        MaxHealth = Player.CurrentHealth.InitialValue;
        position = new float[3];
        position[0] = Player.transform.position.x;
        position[1] = Player.transform.position.y;
        position[2] = Player.transform.position.z;
        foreach (var item in Inv.MyInventory)
        {
            itemsId.Add(item.id);
        }
    }
}
