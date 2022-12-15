using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    bool move = false;

    IEnumerator waiter(){
        //play idle anim
        yield return new WaitForSeconds(1);
        move = true;
    }

    public override void OnEnter(EnemyStateManager esm){
        slime = esm.GetComponent<Slime>();
        player = GameObject.Find("Player");
        pos = esm.GetComponent<Transform>();
        StartCoroutine(waiter());
    }

    public override void OnUpdate(EnemyStateManager esm){
        float dist = Vector2.Distance(player.transform.position, pos.position);
        if(move && dist < slime.detectRange){
            esm.SwitchState(esm.FollowState);
        }
    }

    public override void OnExit(EnemyStateManager esm){

    }

    // public override void OnCollide(EnemyStateManager esm, Collision2D col){
    //     Debug.Log("Collision");
    //     switch(col.gameObject.layer){
    //         case 6:
    //             //col.gameObject.TakeDamage(10); IDK how you implementing that
    //             break;
    //         case 7:
    //             slime.HP -= 10;
    //             break;
    //     }
    // }
}
