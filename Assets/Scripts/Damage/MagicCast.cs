using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCast : MonoBehaviour
{
    public RangeDamage ThisDamage;
    private Animator Anim;
    private Rigidbody2D Rig;
    void Start()
    {
        Anim = GetComponent<Animator>();
        Rig = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.timeScale == 0f)
        {
            return;
        }
        if (!collision.CompareTag("Player") && !collision.CompareTag("Spawner") 
            && !collision.CompareTag("Scaner")
            && !collision.CompareTag("PlayerDamage"))
        {
            if (Rig)
            {
                Rig.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            StartCoroutine(AttackCo());
        }
    }

    private IEnumerator AttackCo()
    {
        if (Anim)
        {
            Anim.SetBool("IsTriggered", true);
        }
        yield return new WaitForSeconds(ThisDamage.WaitTime);
        Destroy(this.gameObject);
    }
}
