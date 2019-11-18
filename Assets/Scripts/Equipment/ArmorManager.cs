using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> Armor;
    private int ActiveArmor;
    public FloatValue PlayerArmor;
    void Start()
    {
        ClearArmor();
        UpdateArmor();
    }

    public void UpdateArmor()
    {
        if (PlayerArmor.InitialValue < ActiveArmor)
        {
            ClearArmor();
        }
        if (PlayerArmor.InitialValue > 10)
        {
            PlayerArmor.InitialValue = 10;
        }
        else if (PlayerArmor.InitialValue < 0)
        {
            PlayerArmor.InitialValue = 0;
        }
        for (int i = ActiveArmor; i < PlayerArmor.InitialValue; i++)
        {
            ActiveArmor++;
            Armor[i].SetActive(true);
        }
    }

    public void ClearArmor()
    {
        for (int i = 0; i < ActiveArmor; i++)
        {
            Armor[i].SetActive(false);
        }
        ActiveArmor = 0;
    }

}
