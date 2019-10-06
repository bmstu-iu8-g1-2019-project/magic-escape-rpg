using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState
{
    walk,
    attack
}
public class PlayerMove : MonoBehaviour
{
    private Animator Animator;
    private Rigidbody2D Body;
    private Vector3 Move;
    private float Angle;
    private Vector3 Ideal = new Vector3(1f, 0f, 0f);
    private Vector3 Buf = new Vector3(0f, 1f, 0f);
    public FloatValue CurrentHealth;
    public PlayerState CurrentState;
    public float MovementSpeed;
    public Rigidbody2D Fireball;
    public Signal PlayerHealthSignal;
    private void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    private void Update()
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

    private void MovePlayer()
    {
        Animator.SetFloat("MoveX", Move.x);
        Animator.SetFloat("MoveY", Move.y);
        Animator.SetBool("IsMove", true);
        Move = Move * Time.deltaTime * MovementSpeed;
        Body.MovePosition(transform.position + Move.normalized * Time.deltaTime * MovementSpeed);
    }

    private void Attack()
    {
        CurrentState = PlayerState.attack;
        Angle = Vector3.Angle(Ideal, Buf);
        if (Buf.y < 0)
        {
            Angle *= -1f;
        }
        StartCoroutine(AttackCo());
        Rigidbody2D FireballClone;
        FireballClone = (Rigidbody2D)Instantiate(Fireball, transform.position, Quaternion.Euler(0f, 0f, Angle));
        FireballClone.AddForce(Buf.normalized * 5f, ForceMode2D.Impulse);
    }

    public void Knock(float KnockTime, float Damage)
    {
        CurrentHealth.RuntimeValue -= Damage;
        PlayerHealthSignal.Raise();
        if (CurrentHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(KnockTime));
        } else {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator KnockCo(float KnockTime)
    {
        yield return new WaitForSeconds(KnockTime);
        Body.velocity = Vector2.zero;
    }

    private IEnumerator AttackCo()
    {
        yield return new WaitForSeconds(0.5f);
        CurrentState = PlayerState.walk;
    }
}
