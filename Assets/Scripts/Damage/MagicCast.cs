using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCast : MonoBehaviour
{
    public WeaponItem ThisItem; // Scriptable object
    public float Speed; // Used in Player Manager
    [SerializeField] private GameObject muzzlePrefab;
    [SerializeField] private GameObject hitPrefab;

    private void Start()
    {
        if (muzzlePrefab)
        {
            var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.forward;
            var ps = muzzleVFX.GetComponent<ParticleSystem>();
            if (ps != null)
                Destroy(muzzleVFX, ps.main.duration);
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.timeScale == 0f)
        {
            return;
        }
        if (CollideCondition(collision))
        {
            Vector3 contact = transform.position;
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normalized);
            if (hitPrefab) {
                var hitVFX = Instantiate(hitPrefab, contact, rot);
                var ps = hitVFX.GetComponent<ParticleSystem>();
                if (!ps)
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
                else
                    Destroy(hitVFX, ps.main.duration);
            }
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            StartCoroutine(AttackCo());
        }
    }

    public virtual bool CollideCondition(Collider2D collision)
    {
        return !collision.CompareTag("Player") && !collision.CompareTag("Spawner")
            && !collision.CompareTag("Scaner")
            && !collision.CompareTag("PlayerDamage") && !collision.CompareTag("Damage");
    }
    private IEnumerator AttackCo()
    {
        yield return new WaitForSeconds(ThisItem.KnockParams.WaitTime);
        Destroy(this.gameObject);
    }
}
