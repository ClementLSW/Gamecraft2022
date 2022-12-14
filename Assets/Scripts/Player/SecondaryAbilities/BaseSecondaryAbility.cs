using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;
public class BaseSecondaryAbility : ScriptableObject
{
    internal Player player;
    public Element element;
    public Sprite icon;
    public virtual BaseSecondary SecondaryState() { return new BaseSecondary(player); }
    [Header("Stats")]
    public float cooldown;
}
