using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Upgrade.asset", menuName = "Upgrades/Wind/Swiftshot")]
public class SwiftshotUpgrade : Upgrade
{
    public override void OnAcquire(Player sm)
    {
        base.OnAcquire(sm);
        sm.primary.fireRate *= 0.8f;
        sm.primary.maxAmmo += 1;
    }
}
