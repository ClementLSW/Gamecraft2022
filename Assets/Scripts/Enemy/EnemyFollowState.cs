using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowState : EnemyBaseState
{
    public GameObject player;
    public Transform pos;
    public Slime slime;

    public override void OnEnter(EnemyStateManager esm){
        player = GameObject.Find("Player");
        slime = esm.GetComponent<Slime>();
        pos = esm.GetComponent<Transform>();
    }

    public override void OnUpdate(EnemyStateManager esm){
        float dist = Vector2.Distance(pos.position, player.transform.position);
        if (dist >= slime.range){
            pos.position = Vector2.MoveTowards(pos.position, player.transform.position, slime.speed * Time.deltaTime);
        }else{
            esm.SwitchState(esm.DashState);
        }
    }

    public override void OnExit(EnemyStateManager esm){

    }

    public override void OnCollide(EnemyStateManager esm, Collision2D col){
        switch(col.gameObject.layer){
            case 6:
                //col.gameObject.TakeDamage(10); IDK how you implementing that
                break;
            case 7:
                slime.HP -= 10;
                break;
        }
    }
}
