﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Panels to show")]
    public GameObject InventoryPanel;
    public GameObject EquipmentPanel;
    public GameObject PausePanel;
    public GameObject AnnouncementPanel;
    public GameObject ShopPanel;

    [Header("Game Pause")]
    private bool GamePaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                GameAwake();
            }
            else
            {
                HideEverything();
                GamePause();
            }
        }
        if (!GamePaused)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (EquipmentPanel.activeSelf)
                {
                    EquipmentPanel.SetActive(!EquipmentPanel.activeSelf);
                }
                if (ShopPanel.activeSelf)
                {
                    ShopPanel.SetActive(!ShopPanel.activeSelf);
                }
                InventoryPanel.SetActive(!InventoryPanel.activeSelf);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (InventoryPanel.activeSelf)
                {
                    InventoryPanel.SetActive(!InventoryPanel.activeSelf);
                }
                if (ShopPanel.activeSelf)
                {
                    ShopPanel.SetActive(!ShopPanel.activeSelf);
                }
                EquipmentPanel.SetActive(!EquipmentPanel.activeSelf);
            }
        }
    }

    public void HideEverything()
    {
        if (InventoryPanel.activeSelf)
        {
            InventoryPanel.SetActive(!InventoryPanel.activeSelf);
        }
        if (EquipmentPanel.activeSelf)
        {
            EquipmentPanel.SetActive(!EquipmentPanel.activeSelf);
        }
        if (ShopPanel.activeSelf)
        {
            ShopPanel.SetActive(!ShopPanel.activeSelf);
        }
    }

    public void GamePause()
    {
        if (!GamePaused)
        {
            Time.timeScale = 0f;
            PausePanel.SetActive(true);
            GamePaused = true;
        }
    }

    public void GameAwake()
    {
        if (GamePaused)
        {
            Time.timeScale = 1f;
            PausePanel.SetActive(false);
            GamePaused = false;
        }
    }

    public void HideAnnouncememt()
    {
        StartCoroutine(HideCo());
    }

    private IEnumerator HideCo()
    {
        yield return new WaitForSeconds(1f);
        AnnouncementPanel.SetActive(false);
    }
}
