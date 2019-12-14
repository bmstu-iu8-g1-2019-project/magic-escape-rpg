using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuffSlot : MonoBehaviour
{
    [Header("UI to change")]
    [SerializeField] private TextMeshProUGUI buffTimer;
    [SerializeField] private Image buffImage;

    [Header("Variables from the Item")]
    private BuffParametrs thisBuff;
    private BuffManager ThisManager;

    [Space]
    public BuffList currentBuffs;

    public void Setup(BuffParametrs newBuff, BuffManager NewManager)
    {
        thisBuff = newBuff;
        ThisManager = NewManager;
        if (thisBuff)
        {
            buffImage.sprite = thisBuff.icon;
        }
    }

    void Update()
    {
        if (thisBuff && thisBuff.timer > 0)
        {
            thisBuff.timer -= Time.deltaTime;
            buffTimer.text = "" + (int)thisBuff.timer;
        }
        else
        {
            ThisManager.Debuff(thisBuff);
            Destroy(this.gameObject);
        }
    }
}
