using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade.asset", menuName = "Upgrades/Base")]
public class Upgrade : ScriptableObject
{
    public Sprite icon;
    public Element element;
    [TextArea(15,20)]
    public string description;
    public Upgrade[] upgradeRequirements;
    public virtual void OnAcquire(Player sm)
    {
        Debug.Log($"{sm.name}: Acquired upgrade {name}");
        //player.buffParticles.gameObject.SetActive(true);
    }
}
