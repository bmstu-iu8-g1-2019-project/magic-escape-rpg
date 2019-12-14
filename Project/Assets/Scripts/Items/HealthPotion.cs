using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private float healValue;
    private GameObject HeartContainer;

    public void RestoreHealth()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<PlayerManager>().CurrentHealth.RuntimeValue += healValue;
        if (Player.GetComponent<PlayerManager>().CurrentHealth.RuntimeValue >
            Player.GetComponent<PlayerManager>().CurrentHealth.InitialValue)
        {
            Player.GetComponent<PlayerManager>().CurrentHealth.RuntimeValue = 
                Player.GetComponent<PlayerManager>().CurrentHealth.InitialValue;
        }
        Player.GetComponent<PlayerManager>().PlayerHealthSignal.Raise();
    }
}
