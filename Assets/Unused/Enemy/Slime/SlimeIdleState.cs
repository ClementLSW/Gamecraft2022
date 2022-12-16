using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIdleState : SlimeBaseState
{
    bool move = false;

    IEnumerator waiter(){
        //play idle anim
        yield return new WaitForSeconds(1);
        move = true;
    }

    public override void OnEnter(SlimeStateManager ssm){
        enemy = ssm.GetComponent<Enemy>();
        player = GameObject.Find("Player");
        pos = ssm.GetComponent<Transform>();
        StartCoroutine(waiter());
    }

    public override void OnUpdate(SlimeStateManager ssm){
        float dist = Vector2.Distance(player.transform.position, pos.position);
        if (move && dist < enemy.detectRange){
            ssm.SwitchState(ssm.FollowState);
        }
    }

    public override void OnExit(SlimeStateManager ssm){

    }
}
