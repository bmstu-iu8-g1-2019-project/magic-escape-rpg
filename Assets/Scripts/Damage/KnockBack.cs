using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float thrust;
    public float KnockTime;
    public FloatValue DamageInit;
    private float Damage;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Player"))
        {
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    hit.GetComponent<Enemy>().CurrentState = EnemyState.stagger;
                    collision.GetComponent<Enemy>().Knock(KnockTime, Damage);
                }
                if (collision.gameObject.CompareTag("Player") && !this.CompareTag("PlayerDamage")) // Prevent dealing damage to yourself
                {
                    hit.GetComponent<PlayerMove>().CurrentState = PlayerState.stagger;
                    collision.GetComponent<PlayerMove>().Knock(KnockTime, Damage);
                }
            }
        }
    }

}
