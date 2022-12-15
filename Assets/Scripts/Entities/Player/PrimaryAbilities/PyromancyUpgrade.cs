using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Upgrade.asset", menuName = "Upgrades/Fire/Pyromancy")]
public class PyromancyUpgrade : Upgrade
{
    public override void OnAcquire(Player sm)
    {
        base.OnAcquire(sm);
        sm.primary.baseDamage += 20;
        sm.primary.burnProc += 7.5f;
    }
}