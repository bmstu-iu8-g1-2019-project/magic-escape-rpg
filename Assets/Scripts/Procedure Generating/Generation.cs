using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    public GameObject room;
    public GameObject Grid;
    private bool IsDrawn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !IsDrawn)
        {
            Instantiate(room, this.transform.position, Quaternion.identity).transform.SetParent(Grid.transform);
            IsDrawn = true;
        }
    }
}
