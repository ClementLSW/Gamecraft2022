using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedFollowState : RangedBaseState
{
    public override void OnEnter(RangedStateManager rsm){
        player = GameObject.Find("Player");
        enemy = rsm.GetComponent<Enemy>();
        pos = rsm.GetComponent<Transform>();
    }
    public override void OnUpdate(RangedStateManager rsm){
        float dist = Vector2.Distance(pos.position, player.transform.position);
        if (dist >= enemy.range){
            pos.position = Vector2.MoveTowards(pos.position, player.transform.position, enemy.speed * Time.deltaTime);
        }else{
            rsm.SwitchState(rsm.FireState);
        }
    }
    public override void OnExit(RangedStateManager rsm){
        
    }
}
