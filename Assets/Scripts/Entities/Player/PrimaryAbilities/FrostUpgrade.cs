using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Upgrade.asset", menuName = "Upgrades/Water/Frost")]
public class FrostUpgrade : Upgrade
{
    public override void OnAcquire(Player sm)
    {
        base.OnAcquire(sm);
        //sm.primary.frostProc += 0.05f;
        sm.frostDur += 0.3f;
        sm.slowStrength += 0.15f;
        sm.frostbiteProc += 0.02f;
    }
}
