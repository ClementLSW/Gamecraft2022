using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffect.asset", menuName = "StatusEffect/Base")]
public class StatusEffect : ScriptableObject
{
    public Sprite icon;
    public float duration;
    public virtual void OnAcquire(BaseEnemy sm)
    {
        //Debug.Log($"{sm.name}: Pickup up {name}");
        //player.buffParticles.gameObject.SetActive(true);
    }
    public virtual void OnExpire(BaseEnemy sm)
    {
        //Debug.Log($"{sm.name}: Finished up {name}");
        //player.buffParticles.gameObject.SetActive(false);
    }
}