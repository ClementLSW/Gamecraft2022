using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyState : BaseState
{
    protected readonly BaseEnemy enemy;
    public BaseEnemyState(BaseEnemy sm) : base(sm)
    {
        enemy = sm;
    }
}
public class MoveState : BaseEnemyState
{
    public MoveState(BaseEnemy sm) : base(sm) { }
    public override void OnEnter()
    {
        base.OnEnter();
        enemy.anim.PlayInFixedTime(BaseEnemy.MoveKey);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        enemy.rb.velocity = GameManager.TimeScale * enemy.moveSpeed * enemy.moveDir;
    }
}



//public class StaggerState : BaseEnemyState
//{
//    readonly float stunDur;
//    readonly float staggerKnockback;
//    readonly float baseStaggerDur = 0.25f;
//    float currentStagger;
//    float currentSpeed;
//    readonly Vector2 knockbackDir;
//    public StaggerState(BaseMob sm, float stagger, Vector2 dir, float _stunDur = 0f) : base(sm)
//    {
//        stunDur = _stunDur;
//        duration = baseStaggerDur + mob.staggerDur + stunDur;
//        staggerKnockback = stagger;
//        knockbackDir = dir;
//        SaveParams(stagger, dir, _stunDur);
//    }
//    public override void OnEnter()
//    {
//        base.OnEnter();
//        mob.queuedState = null;
//        mob.GetStaggered();
//        mob.anim.PlayInFixedTime(BaseMob.StaggerKey);
//        if (stunDur == 0f)
//            mob.sr.material.color = GlobalProperties.i.staggerMatColour;
//        else
//            mob.sr.material.color = GlobalProperties.i.brokenMatColour;
//        AudioManager.i.PlaySFX(mob.hitSound);
//    }
//    public override void Update()
//    {
//        base.Update();
//        currentStagger += Time.deltaTime / (baseStaggerDur + mob.staggerDur);
//        currentSpeed = Mathf.SmoothStep(staggerKnockback, 0, currentStagger);
//    }
//    public override void FixedUpdate()
//    {
//        base.FixedUpdate();
//        mob.rb.velocity = currentSpeed * knockbackDir;
//    }
//    public override void OnExit()
//    {
//        base.OnExit();
//        mob.sr.material.color = Color.white;
//    }
//}

