using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullHealthPotion : MonoBehaviour
{
    public void OnUse()
    {
        PlayerManager player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        player.CurrentHealth.RuntimeValue = player.CurrentHealth.InitialValue;
        player.PlayerHealthSignal.Raise();
    }
}
