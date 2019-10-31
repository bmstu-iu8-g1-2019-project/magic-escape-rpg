using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    [SerializeField] private Rooms RoomsList;
    [SerializeField] private GameObject BlockedRoom;
    private GameObject Spawned;
    private GameObject Grid;
    public string Way;
    public Rooms CornerRooms;
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

                /*if (BlockedRoom && Grid)
                {
                    Instantiate(BlockedRoom, transform.position, Quaternion.identity).transform.SetParent(Grid.transform);
                }*/
                for (int i = 0; i < CornerRooms.Room.Count; i++)
                {
                    List<string> RoomsWays = CornerRooms.Room[i].GetComponent<RoomWays>().Ways;
                    if ((RoomsWays[0] == Way || RoomsWays[0] == collision.GetComponent<Generation>().Way) 
                        && (RoomsWays[0] == Way || RoomsWays[0] == collision.GetComponent<Generation>().Way))
                    {
                        Instantiate(CornerRooms.Room[i], transform.position, Quaternion.identity).transform.SetParent(Grid.transform);
                        Spawned = CornerRooms.Room[i];
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
    private void Spawn()
    {
        if (!IsDrawn)
        { 
            Instantiate(Spawned, this.transform.position, Quaternion.identity).transform.SetParent(Grid.transform);
            IsDrawn = true;
        }
    }
}
