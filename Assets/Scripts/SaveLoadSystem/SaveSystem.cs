﻿using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(PlayerManager Player, PlayerInventory Inv, PlayerEquipment Equipment, PlayerInventory Shop, BuffList currentBuffs)
    {
        BinaryFormatter Formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(Player, Inv, Equipment, Shop, currentBuffs);
        Formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPLayer()
    {
        string path = Application.persistentDataPath + "/player.data";
        if (!File.Exists(path))
        {
            EmergencyRestore();
        }
        BinaryFormatter Formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        PlayerData data = Formatter.Deserialize(stream) as PlayerData;
        stream.Close();
        return data;
    }

    public static void EmergencyRestore()
    {
        SaveLoadActions sys = GameObject.FindGameObjectWithTag("GameController").GetComponent<SaveLoadActions>();
        sys.ResetDefaults();
    }
}
