using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    [SerializeField] private bool IsEnetered = false;
    [SerializeField] private GameObject[] Gates;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !IsEnetered)
        {
            Invoke("CloseGates", 0.3f);
            IsEnetered = true;
        }
    }

    public void OpenGates()
    {
        gameObject.SetActive(false);
    }

    public void CloseGates()
    {
        for (int i = 0; i < Gates.Length; i++)
        {
            Instantiate(Gates[i], transform.position, Quaternion.identity).transform.SetParent(transform);
        }
    }
}
