using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade.asset", menuName = "StatemachineExtensions/Upgrade")]
public class Upgrade : ScriptableObject
{
    public Sprite icon;
    public string description;
    public Upgrade[] upgradeRequirements;
    public virtual void OnAcquire(Player sm)
    {
        Debug.Log($"{sm.name}: Pickup up {name}");
        //player.buffParticles.gameObject.SetActive(true);
    }
}