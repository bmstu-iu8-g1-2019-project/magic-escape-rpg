using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    public Rooms RoomsList;
    public GameObject BlockedRoom;
    private GameObject Spawned;
    private GameObject Grid;
    private bool IsDrawn = false;

    private void Start()
    {
        Grid = GameObject.Find("Basic Grid");
        Spawned = RoomsList.Room[Random.Range(0, RoomsList.Room.Count - 1)];
        Invoke("Spawn", 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spawner") && transform.position != Vector3.zero)
        {
            if (collision.gameObject.GetComponent<Generation>().IsDrawn == false && IsDrawn == false)
            {
                Instantiate(BlockedRoom, transform.position, Quaternion.identity).transform.SetParent(Grid.transform);
                Spawned = BlockedRoom;
                IsDrawn = true;
                collision.gameObject.GetComponent<Generation>().IsDrawn = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void Spawn()
    {
        if (!IsDrawn)
        { 
            Instantiate(Spawned, this.transform.position, Quaternion.identity).transform.SetParent(Grid.transform);
            IsDrawn = true;
        }
    }
}
