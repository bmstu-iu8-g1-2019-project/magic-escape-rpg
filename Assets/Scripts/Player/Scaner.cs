using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaner : MonoBehaviour
{
    private GameObject Character;
    [HideInInspector] public CircleCollider2D Collider;
    [SerializeField] private float deltaR;
    [SerializeField] private float MaxR;

    public virtual void Start()
    {
        Character = GameObject.FindGameObjectWithTag("Player");
        Collider = gameObject.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (Collider.radius < MaxR)
        {
            Collider.radius += deltaR * Time.deltaTime;
        }
        else
        {
            AssignTarget(null);
        }
    }

    public virtual void AssignTarget(GameObject target)
    {
        Character.GetComponent<PlayerManager>().Target = target;
    }
        

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !collision.GetComponent<Enemy>().IsDead())
        {
            AssignTarget(collision.gameObject);
            Collider.radius = 0;
        }
    }
}
