using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedFireState : RangedBaseState
{
    private bool fireComplete = false;
    public GameObject enemyProjectile;

    IEnumerator Fire(Transform pos, Vector3 target){
        yield return new WaitForSeconds(.4f);
        Vector3 dir = (target - pos.position).normalized;
        Instantiate(enemyProjectile, pos.position + (dir * 0.2f), Quaternion.LookRotation(dir));
        Debug.Log("Fire");
        fireComplete = true;
    }
    public override void OnEnter(RangedStateManager rsm){
        player = GameObject.Find("Player");
        pos = rsm.GetComponent<Transform>();
        enemy = rsm.GetComponent<Enemy>();
        fireComplete = false;
        StartCoroutine(Fire(pos, player.transform.position));
    }
    public override void OnUpdate(RangedStateManager rsm){
        if(fireComplete){
            rsm.SwitchState(rsm.RetreatState);
        }
    }
    public override void OnExit(RangedStateManager rsm){
        
    }
}
