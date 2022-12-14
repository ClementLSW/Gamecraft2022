using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;
[CreateAssetMenu(fileName = "WindDashAbility.asset", menuName = "SecondaryAbilities/Wind/WindDashAbility")]
public class WindDashAbility : BaseSecondaryAbility
{
    public override BaseSecondary SecondaryState() => new WindDashState(player);
    public float dashSpeed;
    public float dashDur;
}
