using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;
[CreateAssetMenu(fileName = "WindShotAbility.asset", menuName = "PrimaryAbilities/Wind/WindshotAbility")]
public class WindShotAbility : BasePrimaryAbility
{
    public override BasePrimary PrimaryState() => new WindShotState(player);
    [Header("Multishot")]
    public int projectiles = 1;
    public float spreadAngle = 0;
    [Header("Piercing")]
    public bool piercing = false;

    public override void ActivatePrimary(Vector2 targetDir)
    {
        var attack = Instantiate(projectilePrefab, player.transform.position, Quaternion.identity);
        attack.targetDir = targetDir;
        attack.range = projectileRange;
        attack.speed = projectileSpeed;
        attack.transform.localScale = Vector3.one * projectileSize;
        attack.piercing = piercing;
        attack.Init();
    }
}