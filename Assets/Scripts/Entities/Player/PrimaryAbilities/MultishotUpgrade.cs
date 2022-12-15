using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Upgrade.asset", menuName = "Upgrades/Wind/Multishot")]
public class MultishotUpgrade : Upgrade
{
    public override void OnAcquire(Player sm)
    {
        base.OnAcquire(sm);
        sm.primary.projectiles += 1;
        sm.primary.spreadAngle += 15f;
        sm.primary.damageScale *= 0.85f;
    }
}