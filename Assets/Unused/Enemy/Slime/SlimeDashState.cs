using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDashState : SlimeBaseState
{
    private bool dashComplete = false;

    IEnumerator Dash(Transform pos, Vector3 target){
        
        // Animate precharge
        yield return new WaitForSeconds(.4f);
        // Animate charge

        while(pos.position != target){
            pos.position = Vector2.MoveTowards(pos.position, target, enemy.speed * 1.5f * Time.deltaTime);
        }
        dashComplete = true;
    }

    public override void OnEnter(SlimeStateManager ssm){
        player = GameObject.Find("Player");
        pos = ssm.GetComponent<Transform>();
        enemy = ssm.GetComponent<Enemy>();
        dashComplete = false;
        StartCoroutine(Dash(pos, player.transform.position));
    }

    public override void OnUpdate(SlimeStateManager ssm){
        if(dashComplete){
            ssm.SwitchState(ssm.RetreatState);
        }
    }

    public override void OnExit(SlimeStateManager ssm){

    }
    
}
