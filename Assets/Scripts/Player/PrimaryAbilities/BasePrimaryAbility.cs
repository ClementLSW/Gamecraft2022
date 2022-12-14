using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;

public class BasePrimaryAbility : ScriptableObject
{
    internal Player player;
    public Element element;
    public Sprite icon;
    public string descrption;
    public virtual BasePrimary PrimaryState() { return new BasePrimary(player); }
    public PlayerProjectile projectilePrefab;

    [Header("Stats")]
    public int damage;
    public float knockback;
    public int maxAmmo;
    public float fireRate;
    public float reloadDur;
    [Header("Projectile Types")]
    public float projectileSpeed;
    public float projectileRange;
    public float projectileSize;

    internal int currentAmmo;
    float reloadTimer;
    internal bool CanActivate => currentAmmo > 0;
    public virtual void Init(Player player)
    {
        this.player = player;
        currentAmmo = maxAmmo;
    }
    public virtual void ActivatePrimary(Vector2 targetDir) { }
    public virtual void ReloadAmmo()
    {
        if (player.currentState is BasePrimary || player.currentState is BaseSecondary || currentAmmo >= maxAmmo)
        {
            player.reloadBar.gameObject.SetActive(false);
            reloadTimer = 0;
        }
        else
        {
            player.reloadBar.gameObject.SetActive(true);
            reloadTimer += Time.deltaTime;
            player.reloadBar.value = reloadTimer / reloadDur;
            if (reloadTimer >= reloadDur)
            {
                currentAmmo = maxAmmo;
                reloadTimer = 0;
            }
        }
    }
}
