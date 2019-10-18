using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarian : Enemy
{
    public float ChaseRadius;
    public float AttackRadius;
    public Transform HomePosition;
    private bool MoveCondition;
    private bool AttackCondition;


    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        CheckDistance();
    }

    public void FlipSprite(bool value)
    {
        Sprite.flipX = value;
    }
    void CheckDistance()
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
            Anim.SetBool("IsWalking", true);
            if (transform.position.x - Target.transform.position.x > 0)
            {
                FlipSprite(true);
            }
            else
            {
                FlipSprite(false);
            }
            transform.position = Vector3.MoveTowards(transform.position,
                                Target.transform.position, MoveSpeed * Time.deltaTime);

        } else {
            Anim.SetBool("IsWalking", false);
        }
    }
}
