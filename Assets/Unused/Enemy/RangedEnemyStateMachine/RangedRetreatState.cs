using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedRetreatState : RangedBaseState
{
    public Vector3 retreatSpot;
    private bool retreatComplete;

    private Vector3 GetRetreatSpot(Vector3 pos){
        Vector3 retreatSpot = pos + new Vector3(Random.Range(-(enemy.range*2), (enemy.range*2)), Random.Range(-(enemy.range*2), (enemy.range*2)), 0);
        return retreatSpot;
    }

    public override void OnEnter(RangedStateManager rsm){
        player = GameObject.Find("Player");
        enemy = rsm.GetComponent<Skeleton>();
        pos = rsm.GetComponent<Transform>();
        retreatSpot = GetRetreatSpot(player.transform.position);
    }
    public override void OnUpdate(RangedStateManager rsm){
        float dist = Vector2.Distance(pos.position, player.transform.position);

        if(pos.position != retreatSpot){
            pos.position = Vector2.MoveTowards(pos.position, retreatSpot, enemy.speed * Time.deltaTime);
        }else{
            Debug.Log("Check Distance");
            if(dist > enemy.range){
                rsm.SwitchState(rsm.FollowState);
            }else{
                retreatSpot = GetRetreatSpot(retreatSpot);
            }
        }
    }
    public override void OnExit(RangedStateManager rsm){
        
    }
}
