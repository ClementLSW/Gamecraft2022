using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDashState : EnemyBaseState
{
    private bool dashComplete = false;

    IEnumerator Dash(Transform pos, Vector3 target){
        
        // Animate precharge
        yield return new WaitForSeconds(.4f);
        // Animate charge

        while(pos.position != target){
            pos.position = Vector2.MoveTowards(pos.position, target, slime.speed * 1.5f * Time.deltaTime);
        }
        dashComplete = true;
    }

    public override void OnEnter(EnemyStateManager esm){
        player = GameObject.Find("Player");
        pos = esm.GetComponent<Transform>();
        slime = esm.GetComponent<Slime>();
        dashComplete = false;
        StartCoroutine(Dash(pos, player.transform.position));
    }

    public override void OnUpdate(EnemyStateManager esm){
        if(dashComplete){
            esm.SwitchState(esm.RetreatState);
        }
    }

    public override void OnExit(EnemyStateManager esm){

    }

    // public override void OnCollide(EnemyStateManager esm, Collision2D col){
    //     Debug.Log("Collision");
    //     Debug.Log("Collided with Layer " + col.gameObject.layer);
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
