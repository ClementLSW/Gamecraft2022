using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;

public class BasePrimaryAbility : MonoBehaviour
{
    internal Player player;
    public Element element;
    public Sprite icon;
    public virtual BasePrimary PrimaryState() { return new BasePrimary(player); }
    [Header("Stats")]
    public int damage;
    public float knockback;
    public int maxAmmo;
    public float fireRate;
    public float reloadDur;
    public float projectileSpeed;
    public float projectileRange;
}
