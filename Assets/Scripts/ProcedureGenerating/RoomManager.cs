using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int EnemiesNum;
    public Signal OpenGates;
    public void EnemyBorn()
    {
        EnemiesNum++;
    }

    public void EnemyDied()
    {
        EnemiesNum--;
        if (EnemiesNum <= 0)
        {
            OpenGates.Raise();
        }
    }
}
