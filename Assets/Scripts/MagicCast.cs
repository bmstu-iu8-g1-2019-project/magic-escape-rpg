using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCast : MonoBehaviour
{
    public float MovementSpeed;
    public int Damage = 5;
    private Animator Anim;
    private Rigidbody2D Rig;
    void Start()
    {
        Anim = GetComponent<Animator>();
        Rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            Rig.constraints = RigidbodyConstraints2D.FreezeAll;
            StartCoroutine(AttackCo());
        }
        if (collision.tag == "Enemy")
        {
            Barbarian script = collision.GetComponent<Barbarian>();
            script.Hurt(Damage);
        }
    }

    private IEnumerator AttackCo()
    {
        Anim.SetBool("IsTriggered", true);
        yield return new WaitForSeconds(0.35f);
        Destroy(this.gameObject);
    }
}
