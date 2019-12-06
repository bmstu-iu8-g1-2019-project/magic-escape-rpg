using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    [SerializeField] private BuffList currentBuffs;
    [SerializeField] private GameObject blankBuffSlot;
    [SerializeField] private GameObject buffArea;
    private PlayerManager player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }
    
    public void Debuff(BuffParametrs buff)
    {
        foreach (BuffParametrs item in currentBuffs.list)
        {
            if (item == buff)
            {
                switch (buff.type)
                {
                    case BuffType.expInc:
                        player.expInc -= buff.value;
                        break;
                    case BuffType.dmgInc:
                        player.damageInc -= buff.value;
                        break;
                    case BuffType.dmgDec:
                        player.damageInc += buff.value;
                        break;
                    case BuffType.speedInc:
                        player.MovementSpeed /= (1 + buff.value);
                        break;
                    case BuffType.speedDec:
                        player.MovementSpeed *= (1 + buff.value);
                        break;
                }
                currentBuffs.list.Remove(buff);
                buff.timer = buff.duration;
                return;
            }
        }
    }

    public void Buff(BuffParametrs buff)
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        }
        foreach (var item in currentBuffs.list)
        {
            if (item == buff)
            {
                item.timer += item.duration;
                return;
            }
        }
        currentBuffs.list.Add(buff);
        SetupSlot(buff);
        AddEffect(buff);
    }

    private void SetupSlot(BuffParametrs buff)
    {
        GameObject temp =
            Instantiate(blankBuffSlot,
            buffArea.transform.position, Quaternion.identity);
        temp.transform.SetParent(buffArea.transform);
        temp.transform.localScale = new Vector3(1, 1, 1);
        BuffSlot newSlot = temp.GetComponent<BuffSlot>();
        if (newSlot)
        {
            newSlot.Setup(buff, this);
        }
    }

    private void AddEffect(BuffParametrs buff)
    {

        switch (buff.type)
        {
            case BuffType.expInc:
                player.expInc += buff.value;
                break;
            case BuffType.dmgInc:
                player.damageInc += buff.value;
                break;
            case BuffType.dmgDec:
                player.damageInc -= buff.value;
                break;
            case BuffType.speedInc:
                player.MovementSpeed *= (1 + buff.value);
                break;
            case BuffType.speedDec:
                player.MovementSpeed /= (1 + buff.value);
                break;
        }
    }
}
