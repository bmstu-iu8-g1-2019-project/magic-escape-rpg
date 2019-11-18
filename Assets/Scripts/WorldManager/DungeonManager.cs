﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private GameObject SpawnPoint;
    [SerializeField] private GameObject GoWhite;
    [SerializeField] private Transform UI;
    [SerializeField] private GameObject MessageBox;
    [SerializeField] private TextMeshProUGUI Message;
    [SerializeField] private GameObject Portal;
    private int EnemyNum = 0;
    private bool isMoved;
    private bool isAllowed = true;

    private void Update()
    {
        if (!isMoved && isAllowed)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Message.text = "";
                MessageBox.SetActive(false);
                MoveToBoss();
                isMoved = true;
            }
        }
    }

    public void IncEnemy()
    {
        EnemyNum++;
    }

    void DecEnemy()
    {
        EnemyNum--;
        if (EnemyNum <= 0)
        {
            isAllowed = true;
            MessageBox.SetActive(true);
            Message.text = "U have cleared whole dungeon. When u will be ready to fight with the boss press E";
        }
    }

    private void MoveToBoss()
    {
        Instantiate(GoWhite).transform.SetParent(UI);
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.transform.position = SpawnPoint.transform.position;
    }

    public void SpawnPortal()
    {
        Instantiate(Portal, transform.position, Quaternion.identity);
    }
}
