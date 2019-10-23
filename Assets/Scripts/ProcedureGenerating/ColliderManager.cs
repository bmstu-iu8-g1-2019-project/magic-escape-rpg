using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    [SerializeField] private bool IsEnetered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !IsEnetered)
        {
            Invoke("CloseGates", 0.3f);
            IsEnetered = true;
        }
    }

    public void LetMeOut()
    {

    }

    public void CloseGates()
    {
        PolygonCollider2D[] arr = this.GetComponents<PolygonCollider2D>();
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i].isTrigger = false;
        }
        
    }
}
