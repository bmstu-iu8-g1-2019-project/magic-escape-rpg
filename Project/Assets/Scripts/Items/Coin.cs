using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool IsTriggered;
    private GameObject Player;
    private float timer;
    [SerializeField] private float lifeTime;
    [SerializeField] private Signal UpdateCoins;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        timer = lifeTime;
    }

    void Update()
    {
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            timer -= Time.deltaTime;
        }
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
