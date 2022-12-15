using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Upgrade.asset", menuName = "Upgrades/Wind/Tailwind")]
public class TailwindUpgrade : Upgrade
{
    public override void OnAcquire(Player sm)
    {
        base.OnAcquire(sm);
        GameManager.Player.primary.damageScale *= 0.8f;
    }
}
