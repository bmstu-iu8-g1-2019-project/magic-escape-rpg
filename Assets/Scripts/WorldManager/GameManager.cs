using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Panels to show")]
    public GameObject InventoryPanel;
    public GameObject EquipmentPanel;
    public GameObject PausePanel;
    public GameObject AnnouncementPanel;

    [Header("Game Pause")]
    private bool GamePaused;

    void Update()
    {

        /* if (Input.GetKeyDown(KeyCode.E))
        {
            DialogueActive = false;
            DialogueBox.SetActive(DialogueActive);
            GameAwake();
        }*/
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                GameAwake();
            }
            else
            {
                GamePause();
            }
            PausePanel.SetActive(!PausePanel.activeSelf);
        }
        if (!GamePaused)
        {
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

    public void GamePause()
    {
        if (!GamePaused)
        {
            Time.timeScale = 0f;
            GamePaused = true;
        }
    }

    public void GameAwake()
    {
        if (GamePaused)
        {
            Time.timeScale = 1f;
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
