﻿using System.Collections;
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
    public EnemyState CurrentState;

    [Header("Movement and attack variables")]
    public float MoveSpeed;
    public float AttackKD;

    [Header("Health variables")]
    public FloatValue MaxHealth;
    public float DeadAnimTime;
    [HideInInspector] public float CurrentHealth;
    [HideInInspector] public float TimeKd;
    [HideInInspector] public SpriteRenderer Sprite;
    [HideInInspector] public Rigidbody2D Body;
    [HideInInspector] public Animator Anim;
    [HideInInspector] public GameObject Target;
    [HideInInspector] public GameObject Parent;

    GameObject GetParent(GameObject obj)
    {
        return obj.transform.parent.gameObject;
    }

    private void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
        Target = GameObject.FindWithTag("Player");
        Anim = GetComponent<Animator>();
        Body = GetComponent<Rigidbody2D>();
        CurrentHealth = MaxHealth.InitialValue;
        CurrentState = EnemyState.walk;
        TimeKd = AttackKD;
        Parent = GetParent(GetParent(gameObject));
        Parent.GetComponent<RoomManager>().EnemyBorn();
    }

    public bool IsDead()
    {
        return CurrentHealth <= 0;
    }

    public void Attack()
    {
        if (CurrentState != EnemyState.stagger && TimeKd >= AttackKD)
        {
            TimeKd = 0;
            StartCoroutine(AttackCo());
        } else {
            TimeKd += Time.deltaTime;
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

    virtual public void Knock(float KnockTime, float Damage)
    {
        if (!IsDead())
        {
            CurrentHealth -= Damage;
            if (!IsDead())
            {
                CurrentState = EnemyState.stagger;
                StartCoroutine(GetDamage());
                StartCoroutine(KnockCo(KnockTime));
            }
            else
            {
                Die();
            }
        }
    }

    public IEnumerator GetDamage()
    {
        Anim.SetBool("IsGettingDamage", true);
        yield return new WaitForSeconds(0.3f);
        Anim.SetBool("IsGettingDamage", false);
    }

    public IEnumerator KnockCo(float KnockTime)
    {
        if (Body != null)
        {
            yield return new WaitForSeconds(KnockTime);
            Body.velocity = Vector2.zero;
            CurrentState = EnemyState.walk;
            Body.velocity = Vector2.zero; // Prevent unstopable impulse
        }
    }

    public void Die()
    {
        Body.constraints = RigidbodyConstraints2D.FreezeAll;
        StartCoroutine(DieCo());
        Parent.GetComponent<RoomManager>().EnemyDied();
        BoxCollider2D[] temp = gameObject.GetComponents<BoxCollider2D>();
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i].enabled = false;
        }
    }
    public IEnumerator DieCo()
    {
        Anim.SetBool("IsDead", true);
        yield return new WaitForSeconds(DeadAnimTime);
        Anim.enabled = false;
        CurrentState = EnemyState.die;
    }

    private void OnMouseDown()
    {
        if (IsDead())
        {
            this.gameObject.SetActive(false); // Will introduce loot panel
        }
    }

    public void FlipSprite(bool value)
    {
        Sprite.flipX = value;
    }

    public void WalkToTarget(Vector3 newPos)
    {
        Anim.SetBool("IsWalking", true);
        if (transform.position.x > Target.transform.position.x)
        {
            FlipSprite(true);
        }
        else
        {
            FlipSprite(false);
        }
        transform.position = newPos;
    }
}
