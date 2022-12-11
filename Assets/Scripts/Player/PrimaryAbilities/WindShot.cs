using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;
public class WindShot : BasePrimaryAbility
{
    public override BasePrimary PrimaryState() => new WindShotState(player);
}
