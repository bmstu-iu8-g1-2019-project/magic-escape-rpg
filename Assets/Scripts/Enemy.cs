using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health;
    public string EnemyName;
    public int BaseAttack;
    public float MoveSpeed;
    public Animator Anim;

    private void Start()
    {
        Anim = GetComponent<Animator>();
    }
    public bool IsDead()
    {
        return Health <= 0;
    }

    public void Hurt(int Attack)
    {
        Health = Health - Attack;
        if (IsDead())
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        Anim.SetBool("IsDead", true);
        yield return new WaitForSeconds(0.75f);
        Destroy(this.gameObject);
    }

}

