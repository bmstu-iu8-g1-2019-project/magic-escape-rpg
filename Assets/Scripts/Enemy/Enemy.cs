using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyState
{
    walk,
    attack,
    stagger,
    die
}
public class Enemy : MonoBehaviour
{
    private float CurrentHealth;
    private SpriteRenderer Sprite;
    private Rigidbody2D rig;
    public FloatValue MaxHealth;
    public Animator Anim;
    public GameObject Target;
    public float MoveSpeed;
    public EnemyState CurrentState;

    private void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
        Target = GameObject.FindWithTag("Player");
        Anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        CurrentHealth = MaxHealth.InitialValue;
        CurrentState = EnemyState.walk;
    }

    public bool IsDead()
    {
        return CurrentHealth <= 0;
    }

    public void FlipSprite(bool value)
    {
        Sprite.flipX = value;
    }

    public void Attack()
    {
        StartCoroutine(AttackCo());
    }
    
    private IEnumerator Die()
    {
        Anim.SetBool("IsDead", true);
        yield return new WaitForSeconds(0.75f);
        this.gameObject.SetActive(false);
    }

    private IEnumerator GetDamage()
    {
        if (CurrentState != EnemyState.stagger)
        {
            Anim.SetBool("IsGettingDamage", true);
            yield return new WaitForSeconds(0.3f);
            Anim.SetBool("IsGettingDamage", false);
        }
    }

    private IEnumerator AttackCo()
    {
        if (CurrentState != EnemyState.attack)
        {
            CurrentState = EnemyState.attack;
            Anim.SetBool("IsAttacking", true);
            yield return new WaitForSeconds(0.5f);
            Anim.SetBool("IsAttacking", false);
            CurrentState = EnemyState.walk;
        }
    }

    public void Knock(float KnockTime, float Damage)
    {
        CurrentHealth -= Damage;
        if (!IsDead())
        { 
            StartCoroutine(GetDamage());
            StartCoroutine(KnockCo(KnockTime));
        }
        else
        {
            CurrentState = EnemyState.die;
            StartCoroutine(Die());
        }
    }

    private IEnumerator KnockCo(float KnockTime)
    {
        CurrentState = EnemyState.stagger;
        yield return new WaitForSeconds(KnockTime);
        rig.velocity = Vector2.zero;
        CurrentState = EnemyState.walk;
    }
}

