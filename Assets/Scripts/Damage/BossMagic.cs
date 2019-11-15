using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMagic : MagicCast
{
    public override bool CollideCondition(Collider2D collision)
    {
        return !collision.CompareTag("Enemy") && !collision.CompareTag("Spawner")
            && !collision.CompareTag("Scaner")
            && !collision.CompareTag("PlayerDamage") && !collision.CompareTag("Damage");
    }
}
