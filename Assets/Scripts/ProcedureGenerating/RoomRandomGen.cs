using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRandomGen : MonoBehaviour
{
    public Rooms Bases;
    private GameObject ChosenBase;
    void Start()
    {
        ChosenBase = Bases.Room[Random.Range(0, Bases.Room.Count)];
        Instantiate(ChosenBase, transform.position, Quaternion.identity).transform.SetParent(transform);
    }

    void Update()
    {
        
    }
}
