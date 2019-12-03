using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public TextMeshProUGUI levelText;
    [SerializeField] private GameObject levelUpEffect;

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
                LevelUp(player);
            }
            if (player.Level < levels_grades.Count)
            {
                levelSlider.value = (float)(player.Stars) / (float)(levels_grades[player.Level]);
            }
            else
            {
                levelSlider.value = 1f;
            }
            levelText.text = "" + player.Level + ": " + player.Stars + " / " + levels_grades[player.Level];
        }
    }

    private IEnumerator levelUpCo(PlayerManager player)
    {
        GameObject temp = Instantiate(levelUpEffect, new Vector2(player.transform.position.x, player.transform.position.y - 0.7f), Quaternion.identity);
        temp.transform.SetParent(player.transform);
        yield return new WaitForSeconds(1f);
        temp.SetActive(false);
    }

    public void LevelUp(PlayerManager player)
    {
        if (player.Level < levels_grades.Count)
        {
            HeartManager mgr = GameObject.FindGameObjectWithTag("HeartContainer").GetComponent<HeartManager>();
            StartCoroutine(levelUpCo(player));
            player.Level++;
            if (mgr.HeartContainers.InitialValue < 10)
            {
                player.CurrentHealth.InitialValue += 2;
                mgr.HeartContainers.InitialValue++;
            }
            levelText.text = "" + player.Level + ": " + player.Stars + " / " + levels_grades[player.Level];
            mgr.InitHearts();
            mgr.UpdateHearts();
        }
    }
}
