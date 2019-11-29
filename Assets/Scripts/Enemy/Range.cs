using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : Enemy
{
    [SerializeField] private float AttackRadius;
    [SerializeField] private float ChaseRadius;
    [SerializeField] private Rigidbody2D Arrow;
    private float Angle;
    private bool AttackCondition;
    private bool MoveCondition;
    private Vector3 way;
    private Vector3 AdditionalWay;

    void Update()
    {
        if (Time.timeScale == 0 || IsDead())
        {
            return;
        }
        if (Target)
        {
            FlipSprite(transform.position.x > Target.transform.position.x);
            Action();
        }
    }

    private void Action()
    {
        AttackCondition = Vector3.Distance(Target.transform.position, transform.position) <= AttackRadius && CurrentState == EnemyState.walk;
        MoveCondition = Vector3.Distance(Target.transform.position, transform.position) > AttackRadius
            && Vector3.Distance(Target.transform.position, transform.position) <= ChaseRadius
            && CurrentState == EnemyState.walk;
        way = Target.transform.position - transform.position;
        if (AttackCondition)
        {
            Anim.SetBool("IsWalking", false);
            if (TimeKd >= AttackKD)
            {
                WalkToTarget(Vector3.MoveTowards(transform.position, AdditionalWay, MoveSpeed * Time.deltaTime));
                MoveCondition = false;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, way, AttackRadius);
                if (hit)
                {
                    
                    if (hit.collider.gameObject == Target)
                    {
                        Debug.DrawRay(transform.position, way * AttackRadius);
                        Attack();
                        SpawntProjectTile(way);
                        AdditionalWay = Vector3.Cross(way, new Vector3(0f, 0f, 1f));
                    }
                    else
                    {
                        Debug.DrawRay(transform.position, AdditionalWay * AttackRadius);
                        WalkToTarget(Vector3.MoveTowards(transform.position, AdditionalWay, MoveSpeed * Time.deltaTime));
                        MoveCondition = false;
                    }
                }
            }
            else
            {
                TimeKd += Time.deltaTime;
            }

        }
        if (MoveCondition)
        {
            WalkToTarget(Vector3.MoveTowards(transform.position,
                 Target.transform.position, MoveSpeed * Time.deltaTime));
        }
        else
        {
            Anim.SetBool("IsWalking", false);
        }
    }

    private void SpawntProjectTile(Vector3 Buf)
    {
        Rigidbody2D ArrowClone;
        Angle = Vector3.Angle(new Vector3(1f, 0f, 0f), Buf);
        if (Buf.y < 0)
        {
            Angle *= -1f;
        }
        ArrowClone = (Rigidbody2D)Instantiate(Arrow, transform.position, Quaternion.Euler(0f, 0f, Angle));
        ArrowClone.AddForce(Buf.normalized * 5f, ForceMode2D.Impulse);
    }
}
