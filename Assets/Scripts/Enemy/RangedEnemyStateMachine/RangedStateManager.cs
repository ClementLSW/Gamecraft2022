using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedStateManager : MonoBehaviour
{
    [Header("Current State")]
    public RangedBaseState currentState;


    public RangedIdleState IdleState = new RangedIdleState();
    public RangedFollowState FollowState = new RangedFollowState();
    public RangedRetreatState RetreatState = new RangedRetreatState();
    public RangedFireState FireState = new RangedFireState();

    void Start(){
        currentState = IdleState;
        currentState.OnEnter(this);
    }

    public void Update(){
        currentState.OnUpdate(this);
    }

    public void SwitchState(RangedBaseState state){
        currentState = state;
        currentState.OnEnter(this);
    }
}
