using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;
public class WindDash : BaseSecondaryAbility
{
    public override BaseSecondary SecondaryState() => new WindDashState(player);
    public float dashSpeed;
    public float dashDur;
<<<<<<< HEAD:Assets/Scripts/Player/SecondaryAbilities/WindDash.cs
}
=======
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
            UI._.InitCharges(charges, AssetDB._.elementAffinity[element].colourProfile);
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
>>>>>>> main:Assets/Scripts/Entities/Player/SecondaryAbilities/WindDashAbility.cs
