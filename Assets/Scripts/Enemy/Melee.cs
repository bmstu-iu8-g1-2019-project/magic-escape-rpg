﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Enemy
{
    public float ChaseRadius;
    public float AttackRadius;
    public Transform HomePosition;
    private bool MoveCondition;
    private bool AttackCondition;


    void Update()
    {
        if (Time.timeScale == 0 || IsDead())
        {
            return;
        }
        Action();
    }

    virtual public void Action()
    {
        AttackCondition = Vector3.Distance(Target.transform.position, transform.position) < AttackRadius;
        MoveCondition = Vector3.Distance(Target.transform.position, transform.position) <= ChaseRadius
            && CurrentState == EnemyState.walk;
        if (AttackCondition)
        {
            Anim.SetBool("IsWalking", false);
            Attack();
        }
        else if (MoveCondition)
        {
            TimeKd = AttackKD;
            WalkToTarget(Vector3.MoveTowards(transform.position,
                            Target.transform.position, MoveSpeed * Time.deltaTime));
        } else {
            Anim.SetBool("IsWalking", false);
        }
    }
}