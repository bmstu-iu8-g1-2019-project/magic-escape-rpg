using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMove : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D body;
    private Vector3 move;
    public float movementSpeed;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        move = Vector3.zero;
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        if (move != Vector3.zero)
        {
            MovePlayer();
        }
        else
        {
            animator.SetBool("IsMove", false);
        }
    }

    void MovePlayer()
    {
        animator.SetFloat("MoveX", move.x);
        animator.SetFloat("MoveY", move.y);
        animator.SetBool("IsMove", true);
        move = move * Time.deltaTime * movementSpeed;
        body.MovePosition(transform.position + move);
    }
}
