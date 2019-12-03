﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool IsTriggered;
    private GameObject Player;
    [SerializeField] private Signal UpdateCoins;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (!IsTriggered && Vector2.Distance(transform.position, Player.transform.position) < 3)
        {
            IsTriggered = true;
        }
        if (IsTriggered)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 8f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.GetComponent<PlayerManager>().Coins++;
            UpdateCoins.Raise();
            Destroy(gameObject);
        }
    }
}
