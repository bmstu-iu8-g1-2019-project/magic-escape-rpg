using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : Enemy
{
    public float AttackRadius;
    public float ChaseRadius;
    public Rigidbody2D Arrow;
    private float Angle;
    private bool AttackCondition;
    private bool MoveCondition;
    void Update()
    {
        if (Time.timeScale == 0 || IsDead())
        {
            return;
        }
        AttackCondition = Vector3.Distance(Target.transform.position, transform.position) <= AttackRadius  && CurrentState == EnemyState.walk;
        MoveCondition = Vector3.Distance(Target.transform.position, transform.position) > AttackRadius 
            && Vector3.Distance(Target.transform.position, transform.position) <= ChaseRadius 
            && CurrentState == EnemyState.walk;
        if (AttackCondition)
        {
            Anim.SetBool("IsWalking", false);
            if (TimeKd >= AttackKD)
            {
                Attack();
                Rigidbody2D ArrowClone;
                Vector3 Buf = (Target.transform.position - transform.position);
                Angle = Vector3.Angle(new Vector3(1f, 0f, 0f), Buf);
                if (Buf.y < 0)
                {
                    Angle *= -1f;
                }
                ArrowClone = (Rigidbody2D)Instantiate(Arrow, transform.position, Quaternion.Euler(0f, 0f, Angle));
                ArrowClone.AddForce(Buf.normalized * 5f, ForceMode2D.Impulse);
            }
            else
            {
                TimeKd += Time.deltaTime;
            }
        }
        if (MoveCondition)
        {
            Anim.SetBool("IsWalking", true);
            WalkToTarget(Vector3.MoveTowards(transform.position,
                 Target.transform.position, MoveSpeed * Time.deltaTime));
        }
        else
        {
            Anim.SetBool("IsWalking", false);
        }
    }
}
