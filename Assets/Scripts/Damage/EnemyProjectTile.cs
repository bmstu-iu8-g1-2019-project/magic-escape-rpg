﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectTile : MonoBehaviour
{
    [SerializeField] float destroyTime;
    private float timer;

    void Start()
    {
        timer = destroyTime;
    }

    void Update()
    {
        if (timer <= 0f)
        {
            Destroy(this.gameObject, 0.1f);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.timeScale == 0f)
        {
            return;
        }
        if (!collision.CompareTag("Enemy") && !collision.CompareTag("Spawner") 
            && !collision.CompareTag("Damage") && !collision.CompareTag("PlayerDamage")
            && !collision.CompareTag("Scaner"))
        {
            Destroy(this.gameObject, 0.1f);
        }
    }
}
