using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFollowState : SlimeBaseState
{
    public override void OnEnter(SlimeStateManager ssm){
        player = GameObject.Find("Player");
        enemy = ssm.GetComponent<Enemy>();
        pos = ssm.GetComponent<Transform>();
    }

    public override void OnUpdate(SlimeStateManager ssm){
        float dist = Vector2.Distance(pos.position, player.transform.position);
        if (dist >= enemy.range){
            pos.position = Vector2.MoveTowards(pos.position, player.transform.position, enemy.speed * Time.deltaTime);
        }else{
            ssm.SwitchState(ssm.DashState);
        }
    }

    public override void OnExit(SlimeStateManager ssm){

    }
}
