using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState
{
    walk,
    attack, 
    stagger
}
public class PlayerManager : MonoBehaviour
{
    [Header("Move and attack variables")]
    public float MovementSpeed;
    public Rigidbody2D Fireball;
    private Vector3 Move;
    private float Angle;
    private Vector3 Ideal = new Vector3(1f, 0f, 0f);
    private Vector3 Buf = new Vector3(0f, 1f, 0f);

    [Header("Interaction variables")]
    public FloatValue CurrentHealth;
    public PlayerState CurrentState;
    public Signal PlayerHealthSignal;
    public Signal EquipmentChangeSignal;
    public PlayerEquipment Equipment;

    [Header("GameComponents")]
    private Animator Animator;
    private Rigidbody2D Body;

    private void Start()
    {
        Equipment.RangeDamageItem = Fireball.GetComponent<MagicCast>().ThisDamage;
        Body = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
        {

            return;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            EquipmentChangeSignal.Raise();
        }
        Move = Vector3.zero;
        Move.x = Input.GetAxisRaw("Horizontal");
        Move.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Attack") && CurrentState != PlayerState.attack
            && CurrentState != PlayerState.stagger)
        {
            Attack();
        } 
        else if (Move != Vector3.zero)
        {
            Buf = Move;
            if (CurrentState == PlayerState.walk)
            {
                MovePlayer();
            } else {
                Animator.SetBool("IsMove", false);
            }
        } else {
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

    private IEnumerator AttackCo()
    {
        yield return new WaitForSeconds(0.3f);
        CurrentState = PlayerState.walk;
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
            Time.timeScale = 0f;
        }
    }

    private IEnumerator KnockCo(float KnockTime)
    {
        if (Body != null)
        {
            yield return new WaitForSeconds(KnockTime);
            Body.velocity = Vector2.zero;
            yield return new WaitForSeconds(0.1f);
            CurrentState = PlayerState.walk;
            Body.velocity = Vector2.zero; // Prevent unstopable impulse
        }
    }
}
