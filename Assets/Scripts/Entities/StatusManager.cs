using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    internal StateMachine sm;
    internal Dictionary<StatusType, float> currentStatus = new();
    public bool HasStatus(StatusType status) => currentStatus.ContainsKey(status);

    float tickCounter;
    private void Awake()
    {
        sm = GetComponent<StateMachine>();
    }
    private void OnDisable()
    {
        ClearStatuses(); // For object pooling
    }
    private void Update()
    {
        List<StatusType> statusToRemove = new();
        var statusList = currentStatus.Keys.ToArray();

        int i = 0;
        for (; i < statusList.Length; i++)
        {
            var status = statusList[i];
            if (currentStatus[status] <= 0)
                statusToRemove.Add(status);
            else if (AssetDB._.statusType[status].timed)
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
            OnStatusExpire(status);
            //sm.buffParticles.gameObject.SetActive(false);
        }

        tickCounter += Time.deltaTime;
        if (tickCounter > GameManager.TickRate)
        {
            tickCounter = 0;
            foreach (var status in currentStatus.Keys)
                OnStatusTick(status);
        }
    }
    public void AddStatus(StatusType status, float duration)
    {
        if (!currentStatus.ContainsKey(status))
        {
            currentStatus.Add(status, 0);
            OnStatusAcquire(status);
        }
        if (AssetDB._.statusType[status].stackable)
            currentStatus[status] += duration;
        else
            currentStatus[status] = duration;
    }
    public void RemoveStatus(StatusType status)
    {
        if (!HasStatus(status)) return;
        OnStatusExpire(status);
        currentStatus.Remove(status);
    }
    public void ClearStatuses()
    {
        foreach (var status in currentStatus.Keys)
        {
            OnStatusExpire(status);
            currentStatus.Remove(status);
        }
    }
    void OnStatusAcquire(StatusType status)
    {
        switch (status)
        {
            case StatusType.Burn:
                break;
            case StatusType.Frost:
                sm.moveSpeed *= 0.5f;
                break;
            case StatusType.Frostbite:
                break;
            case StatusType.Shockwave:
                // change tag to shockwaved
                break;
        }
    }
    void OnStatusTick(StatusType status)
    {
        switch (status)
        {
            case StatusType.Burn:
                sm.health -= Mathf.CeilToInt(GameManager.Player.primary.baseDamage * GameManager.Player.burnDamageRatio * GameManager.TickRate);
                break;
            case StatusType.Frost:
                break;
            case StatusType.Frostbite:
                if (currentStatus[status] >= 1)
                    sm.health -= Mathf.CeilToInt(GameManager.Player.frostbiteDamageRatio * sm.baseHealth);
                break;
            case StatusType.Shockwave:
                break;
        }
    }
    void OnStatusExpire(StatusType status)
    {
        switch (status)
        {
            case StatusType.Burn:
                break;
            case StatusType.Frost:
                sm.moveSpeed = sm.baseMoveSpeed;
                RemoveStatus(StatusType.Frostbite);
                break;
            case StatusType.Frostbite:
                break;
            case StatusType.Shockwave:
                // change tag back
                break;
        }
    }
}