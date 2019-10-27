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

    [Header("Movement and attack variables")]
    public float MoveSpeed;
    public float AttackKD;

    [Header("Health variables")]
    public FloatValue MaxHealth;
    public float DeadAnimTime;
    private float CurrentHealth;

    [Header("Interaction variables")]
    private GameObject LootPanel;

    [Header("Work fields")]
    public float TimeKd;

    [Header("GameObjects")]
    public SpriteRenderer Sprite;
    public Rigidbody2D Body;
    public Animator Anim;
    public GameObject Target;

    [Header("Dungeon signal. May be unnecessary")]
    public GameObject Parent;



    private void Start()
    {
        LootPanel = GameObject.FindGameObjectWithTag("Loot");
        Sprite = GetComponent<SpriteRenderer>();
        Target = GameObject.FindWithTag("Player");
        Anim = GetComponent<Animator>();
        Body = GetComponent<Rigidbody2D>();
        CurrentHealth = MaxHealth.InitialValue;
        CurrentState = EnemyState.walk;
        TimeKd = AttackKD;
        Parent = transform.parent.gameObject;
        Parent = Parent.transform.parent.gameObject;
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
        yield return new WaitForSeconds(DeadAnimTime);
        if (IsDead())
        {
            Parent.GetComponent<RoomManager>().EnemyDied();
        }
        CurrentState = EnemyState.die;
        BoxCollider2D[] temp = gameObject.GetComponents<BoxCollider2D>();
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i].enabled = false;
        }
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
                Body.constraints = RigidbodyConstraints2D.FreezeAll;
                StartCoroutine(Die());
            }
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

    public void FlipSprite(bool value)
    {
        Sprite.flipX = value;
    }

    public void WalkToTarget(Vector3 newPos)
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
        transform.position = newPos;
    }
}

