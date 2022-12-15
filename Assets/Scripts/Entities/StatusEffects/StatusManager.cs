using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    internal BaseEnemy sm; // not gamer enough to architype this system for future proofing all statemachines
    internal Dictionary<StatusEffect, float> currentStatus = new Dictionary<StatusEffect, float>();
    private void Awake()
    {
        sm = GetComponent<BaseEnemy>();
    }
    private void OnEnable()
    {
        StatusDB._.StatusTick += OnStatusTick;
    }
    private void OnDisable()
    {
        ClearStatus();
        StatusDB._.StatusTick -= OnStatusTick;
    }
    private void Update()
    {
        List<StatusEffect> statusToRemove = new List<StatusEffect>();
        var statusList = currentStatus.Keys.ToArray();

        int i = 0;
        for (; i < statusList.Length; i++)
        {
            var status = statusList[i];
            if (currentStatus[status] <= 0)
                statusToRemove.Add(status);
            else
                currentStatus[status] -= Time.deltaTime * GameManager.TimeScale;

            //sm.buffIcons[i].gameObject.SetActive(true); //setting active buffs onto ui
            //sm.ui.buffIcons[i].sprite = buffDetails.icon;
            //sm.ui.buffIcons[i].color = new Color(1, 1, 1, KongrooUtils.RemapRange(buffs[buffDetails], 0, buffDetails.duration, 0, 1));
        }
        //for (; i < sm.ui.buffIcons.Length; i++)
        //    sm.ui.buffIcons[i].gameObject.SetActive(false);
        foreach (var status in statusToRemove)
        {
            currentStatus.Remove(status);
            status.OnExpire(sm);
            //sm.buffParticles.gameObject.SetActive(false);
        }
    }
    public void AddStatus(StatusEffect effect)
    {
        if (!currentStatus.ContainsKey(effect))
        {
            currentStatus.Add(effect, 0);
            effect.OnAcquire(sm);
        }
        currentStatus[effect] += effect.duration;
    }
    public bool HasStatus(StatusEffect effect)
    {
        return currentStatus.ContainsKey(effect);
    }
    public void ClearStatus()
    {
        foreach (var status in currentStatus.Keys)
        {
            status.OnExpire(sm);
            currentStatus.Remove(status);
        }
    }
    void OnStatusTick()
    {
        if (HasStatus(StatusDB._.burn))
            sm.currentHealth -= Mathf.CeilToInt(GameManager.Player.primary.baseDamage * GameManager.Player.primary.burnDps * StatusDB.tickRate);
    }
}