using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeDB : MonoBehaviour
{
    public static UpgradeDB _;
    [Header("Fire Primary")]
    public Upgrade combustion;
    public Upgrade pyromancy1, pyromancy2, pyromancy3, pyromancy4, pyromancy5;
    public Upgrade pyromaniac1, pyromaniac2, pyromaniac3, pyromaniac4, pyromaniac5;
    [Header("Wind Primary")]
    public Upgrade tailwind;
    public Upgrade multishot1, multishot2, multishot3, multishot4, multishot5;
    public Upgrade swiftshot1, swiftshot2, swiftshot3, swiftshot4, swiftshot5;
    [Header("Earth Primary")]
    public Upgrade dog;
    public Upgrade stagger1, stagger2, stagger3, stagger4, stagger5;
    public Upgrade shockwave1, shockwave2, shockwave3, shockwave4, shockwave5;
    [Header("Water Primary")]
    public Upgrade holywater;
    public Upgrade pierce1, pierce2, pierce3, pierce4, pierce5;
    public Upgrade frost1, frost2, frost3, frost4, frost5;
    [Header("Utility Upgrades")]
    [Range(0, 1)] public float utilityUpgradeWeight = 0.5f;
    public Upgrade health1, health2, health3;
    public Upgrade reload1, reload2, reload3;
    public Upgrade haste1, haste2, haste3;
    public Upgrade speed1, speed2, speed3;
    public Upgrade cooldown1, cooldown2, cooldown3;
    public Upgrade succ1, succ2, succ3;
    public Upgrade xp1, xp2, xp3;
    [Header("Wind Secondary")]
    public Upgrade windEvolution;
    public Upgrade windDash1, windDash2, windDash3, windDash4, windDash5;

    public static HashSet<Upgrade> Upgrades;

    private void Awake()
    {
        if (_ == null)
        {
            _ = this;
            DontDestroyOnLoad(gameObject);
            RunOnce();
        }
        else
            Destroy(gameObject);
    }
    void RunOnce()
    {
        Upgrades = new();
        var props = typeof(UpgradeDB).GetFields();
        foreach (var prop in props)
        {
            var propValue = prop.GetValue(this);
            if (propValue is Upgrade upgrade)
                Upgrades.Add(upgrade);
        }
    }
}
