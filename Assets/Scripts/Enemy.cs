using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float Health;
    private SpriteRenderer Sprite;
    public FloatValue MaxHealth;
    public string EnemyName;
    public int BaseAttack;
    public float MoveSpeed;
    public Animator Anim;
    public Transform Target;

    private void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
        Target = GameObject.FindWithTag("Player").transform;
        Anim = GetComponent<Animator>();
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

}

