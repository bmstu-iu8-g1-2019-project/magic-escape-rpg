using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarian : Enemy
{
    public float ChaseRadius;
    public float AttackRadius;
    public Transform HomePosition;


    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if (Vector3.Distance(Target.position, transform.position) <= ChaseRadius
            && Vector3.Distance(Target.position, transform.position) >= AttackRadius && !IsDead() && !Anim.GetBool("IsGettingDamage"))
        {
            Anim.SetBool("IsWalking", true);
            if (transform.position.x - Target.position.x > 0)
            {
                FlipSprite(true);
            }
            else
            {
                FlipSprite(false);
            }
            transform.position = Vector3.MoveTowards(transform.position,
                                Target.position, MoveSpeed * Time.deltaTime);

        }
        else
        {
            Anim.SetBool("IsWalking", false);
        }
    }
}
