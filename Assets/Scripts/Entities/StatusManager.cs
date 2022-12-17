using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class StatusInfo
{
    public float timer;
    public int stacks;
    public StatusInfo()
    {
        timer= 0;
        stacks= 0;
    }
}


public class StatusManager : MonoBehaviour
{

    public int frostBiteMax = 20;
    //[ColorUsage(true, true)]
    //public Color shockWaveColor = new Color(0.58823529411f, 0.29411764705f, 0);


    //Color shockWaveColor = new Color(5.8f, 2.9f, 0, 1f);
    Color shockWaveColor = new Color(0, 1f, 0, 1f);

    internal StateMachine sm;
    internal Dictionary<StatusType, StatusInfo> currentStatus = new();
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
            if (AssetDB._.statusType[status].timed)
            {
                if (currentStatus[status].timer <= 0)
                    statusToRemove.Add(status);
                currentStatus[status].timer -= Time.deltaTime * GameManager.TimeScale;
            }

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
    public void AddStatus(StatusType status, StatusInfo source)
    {
        if (!currentStatus.ContainsKey(status))
            currentStatus.Add(status, source);
        if (AssetDB._.statusType[status].stackable)
            currentStatus[status].stacks += source.stacks;
        else if (AssetDB._.statusType[status].stackable && AssetDB._.statusType[status].timed)
            currentStatus[status].timer += source.timer;
        else
            currentStatus[status].timer = source.timer;

        OnStatusAcquire(status);
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
        }
        currentStatus.Clear();

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
                if (currentStatus[status].stacks >= frostBiteMax)
                    sm.health -= Mathf.CeilToInt(GameManager.Player.frostbiteDamageRatio * sm.baseHealth);
                break;
            case StatusType.Shockwave:
                // change tag to shockwaved
                //if (sm is BaseEnemy enemy)
                //{
                //    foreach (var s in enemy.sr)
                //        s.color = shockWaveColor;
                //}
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
                //if (sm is BaseEnemy enemy)
                //{
                //    foreach (var s in enemy.sr)
                //        s.color = Color.black;
                //}
                break;
        }
    }
}