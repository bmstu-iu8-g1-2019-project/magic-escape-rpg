using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    public bool IsEnetered = false;
    [SerializeField] private GameObject[] Gates;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !IsEnetered && collision.transform && !collision.isTrigger)
        {
            Invoke("CloseGates", 0.1f);
            IsEnetered = true;
        }
    }

    public void OpenGates()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void CloseGates()
    {
        for (int i = 0; i < Gates.Length; i++)
        {
            Instantiate(Gates[i], transform.position, Quaternion.identity).transform.SetParent(transform);
        }
    }
}
