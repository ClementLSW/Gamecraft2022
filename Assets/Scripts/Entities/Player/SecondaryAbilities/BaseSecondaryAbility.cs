using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;
public class SecondaryAbility : Upgrade
{
    internal Player player;
    public virtual BaseSecondary SecondaryState() { return new BaseSecondary(player); }
    [Header("Stats")]
    public float cooldown;
    public int charges;
    internal float currentCooldown;
    internal int currentCharges;
    internal bool CanActivate => currentCharges > 0;
    public override void OnAcquire(Player sm)
    {
        base.OnAcquire(sm);
        player = sm;
        currentCooldown = cooldown;
        currentCharges = charges;
        if (charges > 1)
            UI._.InitCharges(charges);
    }
    public virtual void ActivateSecondary()
    {
        currentCharges--;
    }
    public virtual void RechargeCooldown()
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
