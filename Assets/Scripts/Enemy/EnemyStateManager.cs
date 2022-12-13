using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    [Header("Current State")]
    public EnemyBaseState currentState;

    public EnemyIdleState IdleState = new EnemyIdleState();
    public EnemyFollowState FollowState = new EnemyFollowState();
    public EnemyRetreatState RetreatState = new EnemyRetreatState();
    public EnemyDashState DashState = new EnemyDashState();

    void Start(){
        currentState = IdleState;

        currentState.OnEnter(this);
    }

    public void Update(){
        currentState.OnUpdate(this);
    }

    public void OnCollision(Collision2D col){
        currentState.OnCollide(this, col);
    }

    public void SwitchState(EnemyBaseState state){
        currentState = state;
        currentState.OnEnter(this);
    }
}
