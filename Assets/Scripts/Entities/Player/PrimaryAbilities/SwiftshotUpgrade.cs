using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Upgrade.asset", menuName = "Upgrades/Wind/Swiftshot")]
public class SwiftshotUpgrade : Upgrade
{
    public override void OnAcquire(Player sm)
    {
        base.OnAcquire(sm);
        GameManager.Player.primary.fireRate *= 0.8f;
        GameManager.Player.primary.maxAmmo += 1;
    }
}
