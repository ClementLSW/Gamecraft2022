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
    public int charges;
    internal int currentCharges;
    internal override bool CanActivate => currentCharges > 0;
    internal override bool ShowSpecial(out int current)
    {
        current = currentCharges;
        return charges > 1;
    }
    public override void OnAcquire(Player sm)
    {
        base.OnAcquire(sm);
        sm.secondary = this;
        currentCharges = charges;
        currentCooldown = cooldown;
        if (ShowSpecial(out _))
            UI._.InitCharges(charges, AssetDB._.elementCol[element].lightTheme);
    }
    public override void ActivateSecondary()
    {
        currentCharges--;
    }
    public override void RechargeCooldown()
    {
        if (currentCharges == charges) return;
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0)
        {
            currentCharges++;
            currentCooldown = cooldown;
        }
    }
}