using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Upgrade.asset", menuName = "Upgrades/Fire/Pyromaniac")]
public class PyromaniacUpgrade : Upgrade
{
    public override void OnAcquire(Player sm)
    {
        base.OnAcquire(sm);
        sm.primary.burnDps = Mathf.CeilToInt(sm.primary.burnDps * 1.2f);
    }
}