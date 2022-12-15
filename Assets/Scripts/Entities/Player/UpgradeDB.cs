using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDB : MonoBehaviour
{
    public static UpgradeDB _;
    public Upgrade baseWeapon;
    [Header("Fire Primary")]
    public Upgrade burnchance1, burnchance2, burnchance3, burnchance4, burnchance5;
    public Upgrade burndmg1, burndmg2, burndmg3, burndmg4, burndmg5;
    public Upgrade combustion;
    [Header("Wind Primary")]
    public Upgrade multishot1, multishot2, multishot3, multishot4, multishot5;
    public Upgrade swiftshot1, swiftshot2, swiftshot3, swiftshot4, swiftshot5;
    public Upgrade tailwind;
    [Header("Earth Primary")]
    public Upgrade size1, size2, size3, size4, size5;
    public Upgrade shockwave1, shockwave2, shockwave3, shockwave4, shockwave5;
    public Upgrade dog;
    [Header("Water Primary")]
    public Upgrade pierce1, pierce2, pierce3, pierce4, pierce5;
    public Upgrade frost1, frost2, frost3, frost4, frost5;
    public Upgrade holywater;
    [Header("Utility Upgrades")]
    public Upgrade health1, health2, health3;
    public Upgrade reload1, reload2, reload3;
    public Upgrade haste1, haste2, haste3;
    public Upgrade speed1, speed2, speed3;
    public Upgrade cooldown1, cooldown2, cooldown3;
    public Upgrade succ1, succ2, succ3;
    public Upgrade xp1, xp2, xp3;

    private void Awake()
    {
        _ = this;
    }
}
