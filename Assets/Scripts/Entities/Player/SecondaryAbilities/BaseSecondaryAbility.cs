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
    internal float currentCooldown;
    internal virtual bool CanActivate => currentCooldown <= 0;
    internal virtual bool ShowCooldown => currentCooldown > 0;
    internal virtual bool ShowSpecial(out int current)
    {
        current = 0;
        return false;
    }
    public override void OnAcquire(Player sm)
    {
        base.OnAcquire(sm);
        if (player.secondary == null)
            UI._.abilityIcon.sprite = icon;
        // Add more logic when evolving secondary
        player = sm;
    }
    public virtual void ActivateSecondary()
    {
        currentCooldown = cooldown;
    }
    public virtual void RechargeCooldown()
    {
        currentCooldown -= Time.deltaTime;
    }
}
