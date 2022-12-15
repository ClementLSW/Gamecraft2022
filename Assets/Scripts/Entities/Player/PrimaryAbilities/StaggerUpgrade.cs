using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Upgrade.asset", menuName = "Upgrades/Earth/Stagger")]
public class StaggerUpgrade : Upgrade
{
    public override void OnAcquire(Player sm)
    {
        base.OnAcquire(sm);
        sm.primary.projectileSize *= 1.1f;
        sm.primary.knockback *= 1.15f;
    }
}
