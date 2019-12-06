using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New list", menuName = "Buff/List")]
public class BuffList : ScriptableObject
{
    public List<BuffParametrs> list = new List<BuffParametrs>();
}
