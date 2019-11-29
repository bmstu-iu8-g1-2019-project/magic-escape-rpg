using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsAdd : MonoBehaviour
{
    private PlayerManager Player;
    [SerializeField] private TextMeshProUGUI CoinsText;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    public void UpdateCoins()
    {
        if (!Player)
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        }
        CoinsText.text = "" + Player.Coins;
    }
}
