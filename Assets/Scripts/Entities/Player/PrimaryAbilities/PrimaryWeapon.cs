using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;

public class PrimaryWeapon : MonoBehaviour
{
    internal Player player;
    public BasePrimary PrimaryState() => new ShootState(player);
    public PlayerProjectile projectilePrefab;

    [Header("Weapon Stats")]
    public int baseDamage = 100;
    public float knockback = 10;
    public int maxAmmo = 6;
    public float fireRate = 0.25f;
    public float reloadDur = 1f;
    public float attackMoveSpeedMultiplier = 0.5f;
    [Header("Projectile Stats")]
    public float damageScale = 1;
    public float projectileSpeed = 5f;
    public float projectileRange = 30f;
    public float projectileSize = 1f;
    public int projectiles = 1;
    public float spreadAngle = 0;
    public int pierce = 0;

    internal int currentAmmo;
    float reloadTimer;
    internal bool CanActivate => currentAmmo > 0;
    public virtual void Init(Player player)
    {
        this.player = player;
        currentAmmo = maxAmmo;
    }
    public virtual void ActivatePrimary(Vector2 targetDir)
    {
        ShootPrimary(targetDir);
        if (player.upgrades.Contains(UpgradeDB._.tailwind))
            StartCoroutine(ShootDelayed(fireRate / 4f, targetDir));
    }
    IEnumerator ShootDelayed(float time, Vector2 targetDir)
    {
        yield return new WaitForSeconds(time);
        ShootPrimary(targetDir);
    }
    void ShootPrimary(Vector2 targetDir)
    {
        var attack = Instantiate(projectilePrefab, player.transform.position, Quaternion.identity);
        attack.targetDir = targetDir;
        attack.range = projectileRange;
        attack.speed = projectileSpeed;
        attack.transform.localScale = Vector3.one * projectileSize;
        attack.pierce = pierce;
        attack.Init();
    }
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
