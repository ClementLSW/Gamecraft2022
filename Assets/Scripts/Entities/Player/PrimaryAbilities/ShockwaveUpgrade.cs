using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Upgrade.asset", menuName = "Upgrades/Earth/Shockwave")]
public class ShockwaveUpgrade : Upgrade
{
    public override void OnAcquire(Player sm)
    {
        base.OnAcquire(sm);
        sm.primary.shockwaveProc += 0.15f;
    }
}
