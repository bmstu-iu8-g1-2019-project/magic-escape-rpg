using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGargoyle : Enemy
{
    [Header("Boss Settings")]
    [SerializeField] private Rigidbody2D ProjectTile;
    [SerializeField] private float jumpWait;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private int jumpsNum;
    [SerializeField] private float circleAttackKD;
    [SerializeField] private GameObject jumpCollider;
    [SerializeField] private GameObject supporters;
    [SerializeField] private List<InventoryItem> shopAdditions;
    [SerializeField] private PlayerInventory shop;
    private bool isSummoned;
    private int jumpsCounter;
    private float runtime;
    private float Angle;
    private bool isJumping;
    private float timer; 
    private Vector3 jumpPoint;

    void Update()
    {
        if (Time.timeScale == 0 || IsDead())
        {
            return;
        }
        if (Target)
        {
            FlipSprite(transform.position.x > Target.transform.position.x);
            if (CurrentHealth > 0.75f * MaxHealth.InitialValue)
            {
                if (timer <= 0f)
                {
                    timer = AttackKD;
                    ActionPhaseFirst();
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }
            else if (jumpsCounter <= jumpsNum)
            {
                if (!jumpCollider.activeSelf)
                {
                    jumpCollider.SetActive(true);
                    deffense = 1f;
                }
                Action();
            }
            else if (CurrentHealth > 0.5f * MaxHealth.InitialValue)
            {
                if (deffense != 0f)
                {
                    jumpCollider.SetActive(false);
                    deffense = 0f;
                }
                if (timer <= 0f)
                {
                    ActionSecondPhase();
                    timer = circleAttackKD;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }
            else
            {
                if (!isSummoned)
                {
                    SummonSupports();
                    isSummoned = true;
                }
                if (timer <= 0f)
                {
                    timer = AttackKD;
                    ActionPhaseFirst();
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }
        }
    }

    private void SummonSupports()
    {
        int rand = Random.Range(3, 6);
        for (int i = 0; i <= rand; i++)
        {
            Vector3 pos = new Vector3(transform.position.x + Random.Range(-10, 10),
                transform.position.y + Random.Range(-10, 10), 0f);
            Instantiate(supporters, pos, Quaternion.identity);
        }
    }

    private void ActionPhaseFirst()
    {
        Attack();
        Vector3 way = Target.transform.position - transform.position;
        SpawnProjectTile(way);
    }

    private void ActionSecondPhase()
    {
        Attack();
        Vector3 forw = new Vector3(1f, 0f, 0f);
        float rand = Random.Range(0, 1.57f);
        for (float alpha = 0f + rand; alpha < 6.28 + rand; alpha += 0.52f)
        {
            SpawnProjectTile(new Vector3(forw.magnitude * Mathf.Cos(alpha),
                forw.magnitude * Mathf.Sin(alpha)));
            Attack();
        }
    }

    private void SpawnProjectTile(Vector3 Buf)
    {
        Rigidbody2D ProjectTIleClone;
        ProjectTIleClone = (Rigidbody2D)Instantiate(ProjectTile, transform.position, Quaternion.identity);
        ProjectTIleClone.AddForce(Buf.normalized * 5f, ForceMode2D.Impulse);

    }

    private void Action()
    {
        Attack();
        MoveSpeed = jumpSpeed;
        if (!isJumping || transform.position == jumpPoint || runtime <= 0)
        {
            jumpPoint = Target.transform.position - transform.position;
            isJumping = true;
            runtime = 0.5f;
            timer = jumpWait;
            jumpsCounter++;
        }
        else if (timer <= 0f)
        {
            runtime -= Time.deltaTime;
            transform.position += jumpPoint.normalized * Time.deltaTime * jumpSpeed;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    public override void Die()
    {
        foreach(InventoryItem item in shopAdditions)
        {
            shop.MyInventory.Add(item);
        }
        Target.GetComponent<PlayerManager>().bossesProgres++;
        base.Die();
    }
}
