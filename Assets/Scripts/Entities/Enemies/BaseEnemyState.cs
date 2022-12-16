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
