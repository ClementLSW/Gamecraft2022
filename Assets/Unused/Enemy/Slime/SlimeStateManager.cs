using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStateManager : MonoBehaviour
{
    [Header("Current State")]
    public SlimeBaseState currentState;


    public SlimeIdleState IdleState = new SlimeIdleState();
    public SlimeFollowState FollowState = new SlimeFollowState();
    public SlimeRetreatState RetreatState = new SlimeRetreatState();
    public SlimeDashState DashState = new SlimeDashState();

    void Start(){
        currentState = IdleState;
        currentState.OnEnter(this);
    }

    public void Update()
    {
        currentState.OnUpdate(this);
    }

    public void SwitchState(SlimeBaseState state){
        currentState = state;
        currentState.OnEnter(this);
    }
}
