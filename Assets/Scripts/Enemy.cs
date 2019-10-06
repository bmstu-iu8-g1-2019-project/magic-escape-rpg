using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float Health;
    private SpriteRenderer Sprite;
    private Rigidbody2D rig;
    public FloatValue MaxHealth;
    public Animator Anim;
    public GameObject Target;
    public float Damage;
    public float MoveSpeed;
    public float thrust;
    public float KnockTime;

    private void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
        Target = GameObject.FindWithTag("Player");
        Anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        Health = MaxHealth.InitialValue;
    }
    public bool IsDead()
    {
        return Health <= 0;
    }

    public void TakeDamage(float Damage)
    {
        StartCoroutine(GetDamage(Damage));
        if (IsDead())
        {
            StartCoroutine(Die());
        }
    }

    public void FlipSprite(bool value)
    {
        Sprite.flipX = value;
    }

    public void Attack(bool IsKnocking)
    {
        StartCoroutine(AttackCo(IsKnocking));
    }
    
    private IEnumerator Die()
    {
        Anim.SetBool("IsDead", true);
        yield return new WaitForSeconds(0.75f);
        this.gameObject.SetActive(false);
    }

    private IEnumerator GetDamage(float Damage)
    {
        if (!Anim.GetBool("IsGettingDamage"))
        {
            Health -= Damage;
            Anim.SetBool("IsGettingDamage", true);
            yield return new WaitForSeconds(0.3f);
            Anim.SetBool("IsGettingDamage", false);
        }
    }

    private IEnumerator AttackCo(bool IsKnocking)
    {
        if (!Anim.GetBool("IsAttacking"))
        {
            Anim.SetBool("IsAttacking", true);
            yield return new WaitForSeconds(0.2f);
            KnockBack();
            yield return new WaitForSeconds(0.3f);
            Anim.SetBool("IsAttacking", false);
        }
    }

    private void KnockBack()
    {
        Rigidbody2D player = Target.GetComponent<Rigidbody2D>();
        Vector2 difference = Target.transform.position - rig.transform.position;
        difference = difference.normalized * thrust;
        player.AddForce(difference, ForceMode2D.Impulse);
        Target.GetComponent<PlayerMove>().Knock(KnockTime, Damage);
    }

}

