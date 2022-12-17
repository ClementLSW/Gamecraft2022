using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class Zawarudo : PostProcessingApplication
{
    public enum State
    {
        PAUSED,
        INCREASE,
        DESCREASE
    }
    public State state = State.PAUSED;
    public static Zawarudo _;
    public AnimationCurve timeInterp;

    public float time = 0;
    public float duration = 1f;
    private float timer = 0f;
    const float maxTime = -Mathf.PI;

    protected override void Awake()
    {
        base.Awake();
        _ = this;
        enabled = false;
    }
    //private void OnDisable()
    //{
    //    enabled = true;
    //}
    public static void StartZaWarudo()
    {
        _.timer = 0;
        _.state = State.INCREASE;
        _.enabled = true;
        GameManager.SetTimeScale(0);
    }
    public static void EndZaWarudo()
    {
        _.timer = _.duration;
        _.state = State.DESCREASE;
        GameManager.SetTimeScale(1);
    }
    private void Update()
    {

        switch (state)
        {
            case State.PAUSED:
                break;
            case State.INCREASE:
                timer += Time.deltaTime;
                if (timer >= duration)
                {
                    state = State.PAUSED;
                    GameManager.OnLevelUp();
                }
                break;
            case State.DESCREASE:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    state = State.PAUSED;
                    enabled = false;
                }

                break;
        }
        material.SetFloat("iTime", timeInterp.Evaluate(timer / duration) * maxTime);
    }

    protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        base.OnRenderImage(source, destination);
    }

}
