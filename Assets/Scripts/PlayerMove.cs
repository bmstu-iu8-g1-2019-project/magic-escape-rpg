using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState
{
    walk,
    attack,
    interact
}
public class PlayerMove : MonoBehaviour
{
    private Animator Animator;
    private Rigidbody2D Body;
    private Vector3 Move;
    private float Angle;
    private Vector3 Ideal = new Vector3(1f, 0f, 0f);
    private Vector3 Buf = new Vector3(0f, 1f, 0f);
    public PlayerState CurrentState;
    public float MovementSpeed;
    public Rigidbody2D Fireball;
    private void Start()
    {
        CurrentState = PlayerState.walk;
        Body = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move = Vector3.zero;
        Move.x = Input.GetAxisRaw("Horizontal");
        Move.y = Input.GetAxisRaw("Vertical");
        if (Move != Vector3.zero)
        {
            Buf = Move;
        }
        if (Input.GetButtonDown("Attack") && CurrentState != PlayerState.attack)
        {
            Attack();
        }
        else if (Move != Vector3.zero)
        {
            MovePlayer();
        }
        else
        {
            Animator.SetBool("IsMove", false);
        }
    }

    private IEnumerator AttackCo()
    {
        CurrentState = PlayerState.attack;
        yield return new WaitForSeconds(1f);
        CurrentState = PlayerState.walk;
    }

    void MovePlayer()
    {
        Animator.SetFloat("MoveX", Move.x);
        Animator.SetFloat("MoveY", Move.y);
        Animator.SetBool("IsMove", true);
        Move = Move * Time.deltaTime * MovementSpeed;
        Body.MovePosition(transform.position + Move.normalized * Time.deltaTime * MovementSpeed);
    }

    void Attack()
    {
        Angle = Vector3.Angle(Ideal, Buf);
        if (Buf.y < 0)
        {
            Angle *= -1f;
        }
        StartCoroutine(AttackCo());
        Rigidbody2D FireballClone;
        FireballClone = (Rigidbody2D)Instantiate(Fireball, transform.position, Quaternion.Euler(0f, 0f, Angle));
        FireballClone.AddForce(Buf * 5f, ForceMode2D.Impulse);
    }
}
