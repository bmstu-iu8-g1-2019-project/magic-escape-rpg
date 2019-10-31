using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject Target;
    public List<Rigidbody2D> Weapons;
    private Vector3 Move;
    private int WeaponIndex;
    private float Angle;
    private Vector3 Ideal = new Vector3(1f, 0f, 0f);
    private Vector3 Buf = new Vector3(0f, 1f, 0f);

    [Header("Interaction variables")]
    public FloatValue CurrentHealth;
    public PlayerState CurrentState;
    public Signal PlayerHealthSignal;
    public Signal EquipmentChangeSignal;
    public Image CurrentWeapon;
    private bool IsInitialized;

    [Header("GameComponents")]
    private Animator Animator;
    private Rigidbody2D Body;

    private void Start()
    {
        WeaponIndex = 0;
        if (Weapons.Count > 0)
        {
            CurrentWeapon.sprite = Weapons[WeaponIndex].GetComponent<MagicCast>().ThisItem.ItemImage;
            IsInitialized = true;
        }
        Body = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Q) && Weapons.Count > 0)
        {
            ChangeCurrentItem();
        }
        Move = Vector3.zero;
        Move.x = Input.GetAxisRaw("Horizontal");
        Move.y = Input.GetAxisRaw("Vertical");
        if (Move != Vector3.zero)
        {
            Buf = Move;
            if (CurrentState != PlayerState.stagger)
            {
                MovePlayer();
            }
            else
            {
                Animator.SetBool("IsMove", false);
            }
        } else{
            Animator.SetBool("IsMove", false);
        }
        if (Input.GetButtonDown("Attack") && CurrentState != PlayerState.attack
            && CurrentState != PlayerState.stagger && Weapons.Count > 0)
        {
            Attack();
        } 
    }

    public void ChangeCurrentItem()
    {
        WeaponIndex = (WeaponIndex + 1) % Weapons.Count;
        CurrentWeapon.sprite = Weapons[WeaponIndex].GetComponent<MagicCast>().ThisItem.ItemImage;
        if (!IsInitialized)
        {
            Vector4 temp = CurrentWeapon.color;
            temp.w += 1;
            CurrentWeapon.color = temp;
        }
    }

    private void MovePlayer()
    {
        Animator.SetFloat("MoveX", Move.x);
        Animator.SetFloat("MoveY", Move.y); 
        Animator.SetBool("IsMove", true);
        Body.MovePosition(transform.position + Move.normalized * Time.deltaTime * MovementSpeed);
    }

    private void Attack()
    {
        CurrentState = PlayerState.attack;
        if (Target)
        {
            if (!Target.GetComponent<Enemy>().IsDead())
            {
                Vector3 dif = Target.transform.position - transform.position;
                Spawn(dif);
                return;
            }
        }
        Spawn(Buf);
    }

    [Obsolete]
    private void Spawn(Vector3 Vec)
    {
        Angle = Vector3.Angle(Ideal, Vec);
        Angle += Weapons[WeaponIndex].gameObject.GetComponent<ParticleSystem>().startRotation;
        if (Vec.y > 0)
        {
            Angle *= -1f;
        }
        Angle *= Mathf.Sign(Weapons[WeaponIndex].gameObject.GetComponent<ParticleSystem>().startRotation);
        StartCoroutine(AttackCo());
        Rigidbody2D ProjectTileClone = Instantiate(Weapons[WeaponIndex], transform.position, Quaternion.Euler(0f, 0f, Angle));
        ProjectTileClone.AddForce(Vec.normalized * ProjectTileClone.GetComponent<MagicCast>().Speed, ForceMode2D.Impulse);
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
