using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    internal StateMachine sm;
    internal Dictionary<StatusEffect, float> status = new Dictionary<StatusEffect, float>();
    private void Update()
    {
        List<StatusEffect> statusToRemove = new List<StatusEffect>();
        var statusList = status.Keys.ToArray();
        int i = 0;

        for (; i < statusList.Length; i++)
        {
            var effect = statusList[i];
            if (status[effect] <= 0)
            {
                statusToRemove.Add(effect);
            }
            else
            {
                status[effect] -= Time.deltaTime;
            }
            //sm.buffIcons[i].gameObject.SetActive(true); //setting active buffs onto ui
            //sm.ui.buffIcons[i].sprite = buffDetails.icon;
            //sm.ui.buffIcons[i].color = new Color(1, 1, 1, KongrooUtils.RemapRange(buffs[buffDetails], 0, buffDetails.duration, 0, 1));
        }

        //for (; i < sm.ui.buffIcons.Length; i++)
        //    sm.ui.buffIcons[i].gameObject.SetActive(false);


        foreach (var effect in statusToRemove)
        {
            status.Remove(effect);
            effect.OnExpire(sm);
            //sm.buffParticles.gameObject.SetActive(false);
        }
    }

    public void AddStatus(StatusEffect effect)
    {
        //AGilityTimer += buff.itemduration;
        if (!status.ContainsKey(effect))
        {
            status.Add(effect, 0);
            effect.OnAcquire(sm);
        }
        status[effect] += effect.duration;
    }

    public bool HasStatus(StatusEffect effect)
    {
        return status.ContainsKey(effect);
    }
}