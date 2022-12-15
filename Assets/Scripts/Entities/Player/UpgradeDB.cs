using System.Collections;
using System.Collections.Generic;
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
    public Upgrade reroll;
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
