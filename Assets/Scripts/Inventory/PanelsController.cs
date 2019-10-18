using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsController : MonoBehaviour
{

    [Header("Panels to show")]
    public GameObject InventoryPanel;
    public GameObject EquipmentPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f)
        {

            return;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryPanel.SetActive(!InventoryPanel.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            EquipmentPanel.SetActive(!EquipmentPanel.activeSelf);
        }
    }
}
