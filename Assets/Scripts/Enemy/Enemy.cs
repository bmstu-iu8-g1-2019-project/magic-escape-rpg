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
    public EnemyState CurrentState;

    [Header("GameOhects")]
    public SpriteRenderer Sprite;
    private Rigidbody2D Body;
    public Animator Anim;
    public GameObject Target;

    [Header("Movement and attack variables")]
    public float TimeKd;
    public float MoveSpeed;
    public float AttackKD;

    [Header("Health variables")]
    public FloatValue MaxHealth;
    private float CurrentHealth;

    [Header("Interaction variables")]
    public GameObject LootPanel;


    private void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
        Target = GameObject.FindWithTag("Player");
        Anim = GetComponent<Animator>();
        Body = GetComponent<Rigidbody2D>();
        CurrentHealth = MaxHealth.InitialValue;
        CurrentState = EnemyState.walk;
        TimeKd = AttackKD;
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
    
    private IEnumerator Die()
    {
        Anim.SetBool("IsDead", true);
        yield return new WaitForSeconds(0.75f);
        CurrentState = EnemyState.die;
        Anim.enabled = false;
    }

    private IEnumerator GetDamage()
    {
        Anim.SetBool("IsGettingDamage", true);
        yield return new WaitForSeconds(0.3f);
        Anim.SetBool("IsGettingDamage", false);
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
            Body.constraints = RigidbodyConstraints2D.FreezeAll;
            StartCoroutine(Die());
        }
    }

    private IEnumerator KnockCo(float KnockTime)
    {
        if (Body != null)
        {
            yield return new WaitForSeconds(KnockTime);
            Body.velocity = Vector2.zero;
            CurrentState = EnemyState.walk;
            Body.velocity = Vector2.zero; // Prevent unstopable impulse
        }
    }

    private void OnMouseDown()
    {
        if (IsDead())
        {
            LootPanel.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}

