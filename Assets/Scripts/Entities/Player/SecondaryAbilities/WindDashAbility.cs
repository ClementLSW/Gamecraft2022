using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;
[CreateAssetMenu(fileName = "WindDashAbility.asset", menuName = "Upgrades/Wind/WindDashAbility")]
public class WindDashAbility : SecondaryAbility
{
    public override BaseSecondary SecondaryState() => new WindDashState(player);
    public float dashSpeed;
    public float dashDur;
}