using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    public string Way;
    [SerializeField] private Rooms CornerRooms;
    [SerializeField] private Rooms RoomsList;
    [SerializeField] private GameObject BlockedRoom;
    [SerializeField] private Signal RoomSpawned;
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
            if (collision.gameObject.GetComponent<Generation>())
            {
                if (!collision.gameObject.GetComponent<Generation>().IsDrawn && !IsDrawn)
                {
                    for (int i = 0; i < CornerRooms.Room.Count; i++)
                    {
                        List<string> RoomsWays = CornerRooms.Room[i].GetComponent<RoomWays>().Ways;
                        if ((RoomsWays[0] == Way || RoomsWays[0] == collision.GetComponent<Generation>().Way)
                            && (RoomsWays[0] == Way || RoomsWays[0] == collision.GetComponent<Generation>().Way))
                        {
                            Spawned = CornerRooms.Room[i];
                            Spawn();
                            break;
                        }
                    }
                    IsDrawn = true;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    private void Spawn()
    {
        if (!IsDrawn)
        {
            RoomSpawned.Raise();
            Instantiate(Spawned, this.transform.position, Quaternion.identity).transform.SetParent(Grid.transform);
            IsDrawn = true;
        }
    }
}
