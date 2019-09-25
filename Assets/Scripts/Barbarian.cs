using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarian : Enemy
{
    private Animator Animate;
    public Transform Target;
    public float ChaseRadius;
    public float AttackRadius;
    public Transform HomePosition;
    void Start()
    {
        Animate = GetComponent<Animator>();
        Target = GameObject.FindWithTag("Player").transform;

    }

    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if (Vector3.Distance(Target.position, transform.position) <= ChaseRadius
            && Vector3.Distance(Target.position, transform.position) >= AttackRadius)
        {
            Animate.SetBool("IsWalking", true);
            transform.position = Vector3.MoveTowards(transform.position,
                                Target.position, MoveSpeed * Time.deltaTime);
        }
        else
        {
            Animate.SetBool("IsWalking", false);
        }
    }
}
