using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Upgrade.asset", menuName = "Upgrades/Earth/Shockwave")]
public class ShockwaveUpgrade : Upgrade
{
    public float ProcChanceIncrease = 0.15f;
    public float CurrentRatio = 0.15f;
    public override void OnAcquire(Player sm)
    {
        base.OnAcquire(sm);
        sm.shockwaveProc += ProcChanceIncrease;
        sm.shockWaveRatio = CurrentRatio;
    }
}