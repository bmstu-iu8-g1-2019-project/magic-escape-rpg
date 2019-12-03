using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatWindow : MonoBehaviour
{
    private PlayerManager player;
    private GameManager mgr;
    private SaveLoadActions signals;
    [SerializeField] private GameObject panel;
    private bool isActivated;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        mgr = GetComponent<GameManager>();
        signals = GetComponent<SaveLoadActions>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            isActivated = true;
        }
        if (isActivated)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                mgr.LevelUp(player);
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                player.Coins += 1000;
                signals.UpdateCoins.Raise();
                signals.UpdateShop.Raise();
            }
        }
    }
}
