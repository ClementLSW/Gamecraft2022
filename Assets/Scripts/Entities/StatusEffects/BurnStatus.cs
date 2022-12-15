using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnStatus : StatusEffect
{
    public override void OnAcquire(BaseEnemy sm)
    {
        base.OnAcquire(sm);
        // red burning particles idk
    }
    public override void OnExpire(BaseEnemy sm)
    {
        base.OnExpire(sm);
    }
}
