using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BuffType
{
    expInc,
    dmgInc,
    dmgDec,
    speedInc,
    speedDec
}

[CreateAssetMenu(fileName = "New buff options", menuName = "Buff/Options")]
public class BuffParametrs : ScriptableObject
{
    public float value;
    public Sprite icon;
    public float duration;
    public float timer;
    public BuffType type;
    public int id;
}
