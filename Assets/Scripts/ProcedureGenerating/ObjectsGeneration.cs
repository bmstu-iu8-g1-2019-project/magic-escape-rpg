using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsGeneration : MonoBehaviour
{
    public GameObject[] Objects;
    private int random;
    void Start()
    {
        random = Random.Range(0, Objects.Length);
        Instantiate(Objects[random], transform.position, Quaternion.identity).transform.SetParent(transform);
    }
}
