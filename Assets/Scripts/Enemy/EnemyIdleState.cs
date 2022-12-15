using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public Slime slime;
    bool move = false;

    IEnumerator waiter(){
        //play idle anim
        yield return new WaitForSeconds(1);
        move = true;
    }

    public override void OnEnter(EnemyStateManager esm){
        slime = esm.GetComponent<Slime>();
        StartCoroutine(waiter());
    }

    public override void OnUpdate(EnemyStateManager esm){
        if(move){
            esm.SwitchState(esm.FollowState);
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
