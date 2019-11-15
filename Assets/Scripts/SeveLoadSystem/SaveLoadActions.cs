using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadActions : MonoBehaviour
{
    private PlayerManager Player;
    [SerializeField] private PlayerInventory Inv;
    [SerializeField] private PlayerInventory Items;
    [SerializeField] private InventoryManager invMgr;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(Player, Inv);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPLayer();
        Player.CurrentHealth.RuntimeValue = data.CurrentHealth;
        Player.CurrentHealth.InitialValue = data.MaxHealth;
        Vector3 pos;
        pos.x = data.position[0];
        pos.y = data.position[1];
        pos.z = data.position[2];
        Player.transform.position = pos;
        Inv.MyInventory.Clear();
        for (int i = 0; i < data.itemsId.Count; i++)
        {
            InventoryItem item = Items.MyInventory[data.itemsId[i]];
            Inv.MyInventory.Add(item);
            invMgr.AddItem(item);
        }
    }
}
