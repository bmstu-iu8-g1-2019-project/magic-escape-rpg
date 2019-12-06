using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private PlayerInventory shop;
    [SerializeField] private GameObject ShopPanel;
    [SerializeField] private GameObject MessageBox;
    [SerializeField] private TextMeshProUGUI MessageText;
    private GameManager mgr;
    private bool isTriggered;
    void Start()
    {
        mgr = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isTriggered)
        {
            if (!ShopPanel.activeSelf)
            {
                mgr.HideEverything();
                ShopPanel.SetActive(true);
                ShowMessage(false, "");
            }
            else
            {
                ShopPanel.SetActive(false);
                ShowMessage(true, "Press 'E' to open shop");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggered = true;
            ShowMessage(true, "Press 'E' to open shop");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggered = false;
            ShowMessage(false, "");
            ShopPanel.SetActive(false);
        }
    }

    private void ShowMessage(bool flag, string message)
    {
        MessageText.text = message;
        MessageBox.SetActive(flag);
    }
}
