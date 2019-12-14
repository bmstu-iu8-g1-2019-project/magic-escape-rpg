using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GargoyleJump : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();
            player.Knock(0f, 100f);
        }
    }
}
