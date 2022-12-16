using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public static EndScreen _;
    public Image panel;
    public Text endText;
    public float fadeoutDur = 2f;
    bool started = false;
    float startTime;
    private void Awake()
    {
        _ = this;
    }
    private void Update()
    {
        if (!started) return;
        Color col = panel.color;
        col.a = Mathf.Lerp(0, 1, Mathf.Clamp01((Time.time - startTime) / fadeoutDur));
        panel.color = col;
        if (Time.time > startTime + fadeoutDur)
            Application.Quit();
    }
    public void PlayEndScreen()
    {
        started = true;
        startTime = Time.time;
    }
}
