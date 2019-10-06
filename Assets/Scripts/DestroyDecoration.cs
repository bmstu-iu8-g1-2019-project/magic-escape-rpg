﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDecoration : MonoBehaviour
{
    private Animator Anim;

    private void Start()
    {
        Anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Damage")
        {
            StartCoroutine(Destroyable());
        }
    }
    private IEnumerator Destroyable()
    {
        Anim.SetBool("IsDestroyed", true);
        yield return new WaitForSeconds(0.768f);
        Destroy(this.gameObject);
    }
}