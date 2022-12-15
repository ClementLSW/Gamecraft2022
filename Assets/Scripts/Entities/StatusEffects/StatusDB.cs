using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusDB : MonoBehaviour
{
    public static StatusDB _;
    public StatusEffect burn;
    public StatusEffect frost;
    public StatusEffect shockwave;
    public float tickRate = 0.5f;
    float tickDur;
    public event Action StatusTick;
    private void Update()
    {
        tickDur += Time.deltaTime;
        if (tickDur > tickRate)
        {
            tickDur = 0;
            StatusTick?.Invoke();
        }
    }
}
