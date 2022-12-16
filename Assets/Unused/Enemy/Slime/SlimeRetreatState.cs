using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeRetreatState : SlimeBaseState
{
    public Vector3 retreatSpot;
    private bool retreatComplete;

    private Vector3 GetRetreatSpot(Vector3 pos){
        Vector3 retreatSpot = pos + new Vector3(Random.Range(-(enemy.range*2), (enemy.range*2)), Random.Range(-(enemy.range*2), (enemy.range*2)), 0);
        return retreatSpot;
    }

    public override void OnEnter(SlimeStateManager ssm){
        player = GameObject.Find("Player");
        enemy = ssm.GetComponent<Slime>();
        pos = ssm.GetComponent<Transform>();
        retreatSpot = GetRetreatSpot(player.transform.position);
    }

    public override void OnUpdate(SlimeStateManager ssm){
        float dist = Vector2.Distance(pos.position, player.transform.position);

        if(pos.position != retreatSpot){
            pos.position = Vector2.MoveTowards(pos.position, retreatSpot, enemy.speed * Time.deltaTime);
        }else{
            Debug.Log("Check Distance");
            if(dist > enemy.range){
                ssm.SwitchState(ssm.FollowState);
            }else{
                retreatSpot = GetRetreatSpot(retreatSpot);
            }
        }
    }

    public override void OnExit(SlimeStateManager esm){

    }

}
