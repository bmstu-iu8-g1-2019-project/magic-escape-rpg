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
        CheckDistance();
    }

    void CheckDistance()
    {
        AttackCondition = Vector3.Distance(Target.transform.position, transform.position) < AttackRadius;
        MoveCondition = Vector3.Distance(Target.transform.position, transform.position) <= ChaseRadius
            && !AttackCondition && !IsDead()
                && !Anim.GetBool("IsGettingDamage") && !Anim.GetBool("IsAttacking");
        if (AttackCondition)
        {
            Anim.SetBool("IsWalking", false);
            Attack(true);
        }
        else if (MoveCondition)
        {
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

        }
        else
        {
            Anim.SetBool("IsWalking", false);
        }
    }
}
