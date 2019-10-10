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
        if (this.isActiveAndEnabled)
        {
            if (collision.CompareTag("Player") && !this.CompareTag("PlayerDamage"))
            {
                StartCoroutine(KnockDelay(collision, this.GetComponent<Rigidbody2D>()));
                collision.GetComponent<PlayerMove>().Knock(KnockTime, Damage);
            }
            if (collision.CompareTag("Enemy") && collision.isTrigger && !this.CompareTag("Damage"))
            {
                Rigidbody2D Player = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
                Knock(collision, Player);
                collision.GetComponent<Enemy>().Knock(KnockTime, Damage);
            }
        }
    }

    private void Knock(Collider2D collision, Rigidbody2D knocker)
    {
        Damage = DamageInit.RuntimeValue;
        Rigidbody2D target = collision.GetComponent<Rigidbody2D>();
        Vector2 difference = target.transform.position - knocker.transform.position;
        difference = difference.normalized * thrust;
        target.AddForce(difference / 2, ForceMode2D.Impulse);
    }

    private IEnumerator KnockDelay(Collider2D collision, Rigidbody2D knocker)
    {
        yield return new WaitForSeconds(0.1f);
        Knock(collision, knocker);
    }
}
