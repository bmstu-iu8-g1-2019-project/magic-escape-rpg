using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScaner : MonoBehaviour
{
    private GameObject Character; // Unity logic cannot inherit it from scaner
    [HideInInspector] public CircleCollider2D Collider;

    private void Start()
    {
        Character = transform.parent.gameObject;
        Collider = gameObject.GetComponent<CircleCollider2D>();
    }

    public void AssignTarget(GameObject target)
    {
        if (Character)
        {
            Character.GetComponent<Enemy>().Target = target;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Decorations"))
        {
            if (collision.gameObject.GetComponent<ColliderManager>() && !collision.gameObject.GetComponent<ColliderManager>().IsEnetered)
            {
                AssignTarget(null);
                return;
            }
        }
        if (collision.CompareTag("Player"))
        {
            AssignTarget(collision.gameObject);
        }
    }
}
