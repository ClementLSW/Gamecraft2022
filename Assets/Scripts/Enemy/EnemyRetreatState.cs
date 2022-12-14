using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRetreatState : EnemyBaseState
{
    public Vector3 retreatSpot;
    private bool retreatComplete;

    private Vector3 GetRetreatSpot(Vector3 pos){
        Vector3 retreatSpot = pos + new Vector3(Random.Range(-(slime.range*2), (slime.range*2)), Random.Range(-(slime.range*2), (slime.range*2)), 0);
        return retreatSpot;
    }

    public override void OnEnter(EnemyStateManager esm){
        player = GameObject.Find("Player");
        slime = esm.GetComponent<Slime>();
        pos = esm.GetComponent<Transform>();
        retreatSpot = GetRetreatSpot(player.transform.position);
    }

    public override void OnUpdate(EnemyStateManager esm){
        float dist = Vector2.Distance(pos.position, player.transform.position);

        if(pos.position != retreatSpot){
            pos.position = Vector2.MoveTowards(pos.position, retreatSpot, slime.speed * Time.deltaTime);
        }else{
            Debug.Log("Check Distance");
            if(dist > slime.range){
                esm.SwitchState(esm.FollowState);
            }else{
                retreatSpot = GetRetreatSpot(retreatSpot);
            }
        }
    }

    public override void OnExit(EnemyStateManager esm){

    }

    // public override void OnCollide(EnemyStateManager esm, Collision2D col){
    //     Debug.Log("Collision");
    //     switch(col.gameObject.layer){
    //         case 7:
    //             slime.HP -= 10;
    //             break;
    //     }
    // }
}
