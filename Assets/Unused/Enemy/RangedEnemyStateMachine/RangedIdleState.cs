using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedIdleState : RangedBaseState
{
    bool move = false;

    IEnumerator waiter(){
        //play idle anim
        yield return new WaitForSeconds(1);
        move = true;
    }

    public override void OnEnter(RangedStateManager rsm){
        enemy = rsm.GetComponent<Enemy>();
        player = GameObject.Find("Player");
        pos = rsm.GetComponent<Transform>();
        StartCoroutine(waiter());
    }
    
    public override void OnUpdate(RangedStateManager rsm){
        float dist = Vector2.Distance(player.transform.position, pos.position);
        if (move && dist < enemy.detectRange){
            rsm.SwitchState(rsm.FollowState);
        }
    }
    public override void OnExit(RangedStateManager rsm){
        
    }
}
