﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Panels to show")]
    public GameObject InventoryPanel;
    public GameObject EquipmentPanel;
    public GameObject PausePanel;
    public GameObject AnnouncementPanel;
    public GameObject ShopPanel;

    [Header("Level variables")]
    public Slider levelSlider;
    public List<int> levels_grades;

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

    public void UpdateLevelSlider()
    {
        PlayerManager player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        if (player.Level < levels_grades.Count)
        {
            if (player.Stars >= levels_grades[player.Level])
            {
                player.Stars -= levels_grades[player.Level];
                player.Level++;
            }
            if (player.Level < levels_grades.Count)
            {
                levelSlider.value = (float)(player.Stars) / (float)(levels_grades[player.Level]);
            }
            else
            {
                levelSlider.value = 1f;
            }
        }
    }
}
