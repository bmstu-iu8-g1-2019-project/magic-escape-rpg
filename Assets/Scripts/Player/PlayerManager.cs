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
    [HideInInspector] public GameObject Target;
    public RigidBodyList Weapons;
    private Vector3 RevertedMove;
    private Vector2 Move;
    private float Angle;
    private Vector3 Ideal = new Vector3(1f, 0f, 0f);
    private Vector3 Buf = new Vector3(0f, 1f, 0f);

    [Header("Interaction variables")]
    public int bossesProgres;
    public FloatValue CurrentHealth;
    public FloatValue Armor;
    [HideInInspector]public PlayerState CurrentState;

    [Header("UI")]
    public GameObject HurtPanel;
    public Image CurrentWeapon;
    private int WeaponIndex;
    private bool IsInitialized; // Weapon

    [Header("Signals")]
    public Signal PlayerHealthSignal;
    public Signal EquipmentChangeSignal;
    public Signal PlayerDeadSignal;
    public Signal UpdateArmor;

    [Header("GameComponents")]
    private Animator Animator;
    private Rigidbody2D Body;
    private float ChangeWeaponKD = 0.01f;
    private float WeaponCurrentKD = 0.1f;
    [HideInInspector] public int Coins;
    [HideInInspector]public int Stars;
    [HideInInspector]public int Level = 1;

    [Header("Settings values")]
    private bool isHelpAim = true;
    private bool isWalkRotated = true;

    [Space]
    [SerializeField] private SaveLoadActions sys;


    public void AimHelpChange()
    {
        isHelpAim = !isHelpAim;
    }

    public void WalkWayChange()
    {
        isWalkRotated = !isWalkRotated;
    }

    private void Start()
    {
        if (sys)
        {
           sys.LoadPlayer();
        }
        WeaponIndex = 0;
        if (Weapons.thisList.Count > 0 && CurrentWeapon)
        {
            CurrentWeapon.sprite = Weapons.thisList[WeaponIndex].GetComponent<MagicCast>().ThisItem.ItemImage;
            Vector4 temp = CurrentWeapon.color;
            temp.w += 1;
            CurrentWeapon.color = temp;
            IsInitialized = true;
        }
        Body = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        CurrentHealth.RuntimeValue = CurrentHealth.InitialValue;
        PlayerHealthSignal.Raise();
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Q) && Weapons.thisList.Count > 0 && ChangeWeaponKD <= WeaponCurrentKD)
        {
            WeaponCurrentKD = 0f;
            ChangeCurrentItem();
        }
        if (Weapons.thisList.Count > 0)
        {
            if (Input.GetButtonDown("Attack") && CurrentState != PlayerState.attack
                && CurrentState != PlayerState.stagger)
            {
                Attack();
            }
        }
    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }
       
        else if (ChangeWeaponKD >= WeaponCurrentKD)
        {
            WeaponCurrentKD += Time.fixedDeltaTime;
        }
        Move.x = Input.GetAxisRaw("Horizontal");
        Move.y = Input.GetAxisRaw("Vertical");
        if (Move != Vector2.zero)
        { 

            if (CurrentState != PlayerState.stagger)
            {
                if (isWalkRotated)
                {
                    RevertedMove = Revert();
                    Buf = RevertedMove;
                    MovePlayer(RevertedMove);
                }
                else
                {
                    Buf = Move;
                    MovePlayer(Move);
                }  
            }
            else
            {
                Animator.SetBool("IsMove", false);
            }
        } else{
            Animator.SetBool("IsMove", false);
        }
    }

    private Vector3 Revert()
    {
        Vector3 way = Vector3.zero;
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1)
        {
            way.x += Move.x * Mathf.Sin(3.14f / 2.85f);
            way.y += Move.x * Mathf.Cos(3.14f / 2.85f);
        }
        if (Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            way.x += Move.y * Mathf.Cos(3.14f / 2.85f + 1.57f);
            way.y += Move.y * Mathf.Sin(3.14f / 2.85f + 1.57f);
        }
        return way;
    }

    public void ChangeCurrentItem()
    {
        if (CurrentWeapon)
        {
            WeaponIndex = (WeaponIndex + 1) % Weapons.thisList.Count;
        }
        CurrentWeapon.sprite = Weapons.thisList[WeaponIndex].GetComponent<MagicCast>().ThisItem.ItemImage;
        if (!IsInitialized)
        {
            SetWeaponAlpha(1);
        }
    }

    public void SetWeaponAlpha(int value)
    {
        if (CurrentWeapon)
        {
            Vector4 temp = CurrentWeapon.color;
            temp.w = value;
            CurrentWeapon.color = temp;
        }
    }

    private void MovePlayer(Vector3 Way)
    {
        Animator.SetFloat("MoveX", Move.x);
        Animator.SetFloat("MoveY", Move.y); 
        Animator.SetBool("IsMove", true);
        Vector2 inputVector = Vector2.ClampMagnitude(Way, 1f);
        Vector2 move = inputVector * MovementSpeed;
        Vector2 newPos = Body.position + move * Time.fixedDeltaTime;
        Body.MovePosition(newPos);
    }

    private void Attack()
    {
        CurrentState = PlayerState.attack;
        if (Target && isHelpAim)
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
        Angle += Weapons.thisList[WeaponIndex].gameObject.GetComponent<ParticleSystem>().startRotation;
        if (Vec.y > 0)
        {
            Angle *= -1f;
        }
        Angle *= Mathf.Sign(Weapons.thisList[WeaponIndex].gameObject.GetComponent<ParticleSystem>().startRotation);
        StartCoroutine(AttackCo());
        Rigidbody2D ProjectTileClone = Instantiate(Weapons.thisList[WeaponIndex], transform.position, Quaternion.Euler(0f, 0f, Angle));
        ProjectTileClone.AddForce(Vec.normalized * ProjectTileClone.GetComponent<MagicCast>().Speed, ForceMode2D.Impulse);
    }

    private IEnumerator AttackCo()
    {
        yield return new WaitForSeconds(0.3f);
        CurrentState = PlayerState.walk;
    }

    public void Knock(float KnockTime, float Damage)
    {
        CurrentHealth.RuntimeValue -= Damage * (1 - ( Armor.InitialValue / 10));
        PlayerHealthSignal.Raise();
        if (CurrentHealth.RuntimeValue > 0)
        {
            StartCoroutine(RaisePanelCo());
            StartCoroutine(KnockCo(KnockTime));
        } else {
            PlayerDeadSignal.Raise();
        }
    }
    private IEnumerator RaisePanelCo()
    {
        HurtPanel.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        HurtPanel.SetActive(false);
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
