using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPrincess : Enemy
{
    [SerializeField] private float MeleeChaseRadius;
    [SerializeField] private float MeleeAttackRadius;
    [SerializeField] private float RangeChaseRadius;
    [SerializeField] private float RangeAttackRadius;
    private bool AttackCondition;
    private bool MoveCondition;
    [SerializeField] Rigidbody2D ProjectTileDirect;
    [SerializeField] Rigidbody2D ProjectTileFollow;
    private void Update()
    {
        if (Time.timeScale == 0 || IsDead())
        {
            return;
        }
        if (Target)
        {
            if (CurrentHealth < MaxHealth.InitialValue / 2)
            {
                MeleeAction();
            }
            else
            {
                RangeAction();
            }
        }
    }

    private void MeleeAction()
    {
        MoveSpeed = Target.GetComponent<PlayerManager>().MovementSpeed + 1.5f;
        AttackCondition = Vector3.Distance(Target.transform.position, transform.position) < MeleeAttackRadius;
        MoveCondition = Vector3.Distance(Target.transform.position, transform.position) <= MeleeChaseRadius
           && CurrentState == EnemyState.walk;
        if (AttackCondition)
        {
            Anim.SetBool("IsWalking", false);
            Attack();
        }
        if (MoveCondition)
        {
            TimeKd = AttackKD;
            WalkToTarget(Vector3.MoveTowards(transform.position,
                            Target.transform.position, MoveSpeed * Time.deltaTime));
        }
        else
        {
            Anim.SetBool("IsWalking", false);
        }
    }

    private void RangeAction()
    {
        AttackCondition = Vector3.Distance(Target.transform.position, transform.position) <= RangeAttackRadius && CurrentState == EnemyState.walk;
        MoveCondition = Vector3.Distance(Target.transform.position, transform.position) > RangeAttackRadius
            && Vector3.Distance(Target.transform.position, transform.position) <= RangeChaseRadius
            && CurrentState == EnemyState.walk;
        if (AttackCondition)
        {
            Anim.SetBool("IsWalking", false);
            if (TimeKd >= AttackKD)
            {
                Attack();
                SpawntProjectTile();
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

    private void SpawntProjectTile()
    {
        float Angle;
        Rigidbody2D ArrowClone;
        Vector3 Buf = (Target.transform.position - transform.position);
        Angle = Vector3.Angle(new Vector3(1f, 0f, 0f), Buf);
        if (Buf.y < 0)
        {
            Angle *= -1f;
        }
        ArrowClone = (Rigidbody2D)Instantiate(ProjectTileDirect, transform.position, Quaternion.Euler(0f, 0f, Angle));
        ArrowClone.AddForce(Buf.normalized * 5f, ForceMode2D.Impulse);
    }

    override public void Knock(float KnockTime, float Damage)
    {
        if (!IsDead())
        {
            CurrentHealth -= Damage;
            if (!IsDead())
            {
                MeleeChaseRadius = 15f; // After getting Damage melee will chase
                CurrentState = EnemyState.stagger;
                StartCoroutine(GetDamage());
                StartCoroutine(KnockCo(KnockTime));
            }
            else
            {
                Body.constraints = RigidbodyConstraints2D.FreezeAll;
                Die();
            }
        }
    }
}
