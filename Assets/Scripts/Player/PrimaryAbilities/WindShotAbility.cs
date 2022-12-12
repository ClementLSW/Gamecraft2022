using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;
[CreateAssetMenu(fileName = "WindShotAbility.asset", menuName = "PrimaryAbilities/WindshotAbility")]
public class WindShotAbility : BasePrimaryAbility
{
    public override BasePrimary PrimaryState() => new WindShotState(player);
    public int projectiles = 1;
    public float spreadAngle = 0;
    public bool piercing = false;
}