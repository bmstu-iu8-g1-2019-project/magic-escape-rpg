using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaner : MonoBehaviour
{
    private GameObject Player;
    private CircleCollider2D Collider;
    [SerializeField] private float deltaR;
    [SerializeField] private float MaxR;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Collider = gameObject.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (Collider.radius < MaxR)
        {
            Collider.radius += deltaR * Time.deltaTime;
        }         
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !collision.GetComponent<Enemy>().IsDead())
        {
            Player.GetComponent<PlayerManager>().Target = collision.gameObject; 
            Collider.radius = 0;
        }
    }
}
