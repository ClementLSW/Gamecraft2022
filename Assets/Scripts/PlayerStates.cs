using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerState : BaseState
{
    protected readonly Player player;
    public BasePlayerState(Player sm) : base(sm)
    {
        player = sm;
    }
}
public class IdleState : BasePlayerState
{
    public IdleState(Player sm) : base(sm) { }
    public override void Update()
    {
        base.Update();
        if (player.moveDir.magnitude > 0)
            sm.ChangeState(new MoveState(player));
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.rb.velocity = Vector2.zero;
    }
}
public class MoveState : BasePlayerState
{
    public MoveState(Player sm) : base(sm) { }
    public override void Update()
    {
        base.Update();
        if (player.moveDir.magnitude == 0)
            sm.ChangeState(new IdleState(player));
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.rb.velocity = player.moveSpeed * player.moveDir;
    }
}
