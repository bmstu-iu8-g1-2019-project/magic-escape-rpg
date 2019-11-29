using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScaner : Scaner
{
    private GameObject Character; // Unity logic cannot inherit it from scaner

    override public void Start()
    {
        Character = transform.parent.gameObject;
        Collider = gameObject.GetComponent<CircleCollider2D>();
    }

    override public void AssignTarget(GameObject target)
    {
        Character.GetComponent<Enemy>().Target = target;
        Collider.radius = 0;
    }

    override public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Decorations"))
        {
            if (collision.gameObject.GetComponent<ColliderManager>() && !collision.gameObject.GetComponent<ColliderManager>().IsEnetered)
            {
                return;
            }
        }
        if (collision.CompareTag("Player"))
        {
            AssignTarget(collision.gameObject);
        }
    }
}
