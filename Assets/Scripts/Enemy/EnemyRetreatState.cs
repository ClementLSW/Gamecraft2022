using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRetreatState : EnemyBaseState
{
    public GameObject player;
    public Transform pos;
    public Slime slime;
    public Vector3 retreatSpot;
    private bool retreatComplete;

    private Vector3 GetRetreatSpot(){
        Vector3 retreatSpot = player.transform.position + new Vector3(Random.Range(-8, 8), Random.Range(-8, 8), 0);
        return retreatSpot;
    }

    public override void OnEnter(EnemyStateManager esm){
        player = GameObject.Find("Player");
        slime = esm.GetComponent<Slime>();
        pos = esm.GetComponent<Transform>();
        retreatSpot = GetRetreatSpot();
    }

    public override void OnUpdate(EnemyStateManager esm){
        float dist = Vector2.Distance(pos.position, player.transform.position);

        if(pos.position != retreatSpot){
            pos.position = Vector2.MoveTowards(pos.position, retreatSpot, slime.speed * Time.deltaTime);
        }else{
            if(dist > 4){
                esm.SwitchState(esm.FollowState);
            }else{
                retreatSpot += retreatSpot;
            }
        }
    }

    public override void OnExit(EnemyStateManager esm){

    }

    public override void OnCollide(EnemyStateManager esm, Collision2D col){
        switch(col.gameObject.layer){
            case 7:
                slime.HP -= 10;
                break;
        }
    }
}
