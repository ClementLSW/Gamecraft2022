using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Upgrade.asset", menuName = "Upgrades/Wind/Multishot")]
public class MultishotUpgrade : Upgrade
{
    public override void OnAcquire(Player sm)
    {
        base.OnAcquire(sm);
        GameManager.Player.primary.damageScale *= 0.9f;
        GameManager.Player.primary.projectiles += 1;
        GameManager.Player.primary.spreadAngle += 15f;
    }
}