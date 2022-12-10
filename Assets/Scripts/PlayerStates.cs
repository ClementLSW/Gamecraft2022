using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class BasePlayerState : BaseState
{
    protected readonly Player player;
    protected readonly float inputBuffer = 0.5f;
    public BasePlayerState(Player sm) : base(sm)
    {
        player = sm;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.rb.velocity = player.moveSpeed * player.moveDir;
    }
}
#region Idle States
public class BaseIdle : BasePlayerState
{
    public BaseIdle(Player sm) : base(sm) { }
}
public class StopState : BaseIdle
{
    public StopState(Player sm) : base(sm) { }
    public override void Update()
    {
        base.Update();
        if (player.moveDir.magnitude > 0)
            sm.ChangeState(new MoveState(player));
    }
    public override void FixedUpdate()
    {
        player.rb.velocity = Vector2.zero;
    }
}
public class MoveState : BaseIdle
{
    public MoveState(Player sm) : base(sm) { }
    public override void Update()
    {
        base.Update();
        if (player.moveDir.magnitude == 0)
            sm.ChangeState(new StopState(player));
    }
}
#endregion
#region Primary Attack States
public class BasePrimary : BasePlayerState
{
    public BasePrimary(Player sm) : base(sm) { }
}
public class WindShot : BasePrimary
{
    public WindShot(Player sm) : base(sm)
    {
        duration = player.primaryRate;
        bufferPoint = false;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        player.ShootProjectile();
    }
    public override void Update()
    {
        base.Update();
        if (age < inputBuffer * duration)
            bufferPoint = true;
    }
    public override void FixedUpdate()
    {
        player.rb.velocity = player.moveSpeed * player.attackMoveSpeedMultiplier * player.moveDir;
    }
}
#endregion
#region Secondary Attack States
public class BaseSecondary : BasePlayerState
{
    public BaseSecondary(Player sm) : base(sm) { }
}
public class WindDash : BaseSecondary
{
    Vector2 dashDir;
    float currentSpeed;
    public WindDash(Player sm) : base(sm)
    {
        duration = player.secondaryRate;
        bufferPoint = false;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        dashDir = player.moveDir == Vector2.zero ? player.lookDir : player.moveDir;
    }
    public override void Update()
    {
        base.Update();
        currentSpeed = Mathf.SmoothStep(player.dashSpeed + player.moveSpeed, player.moveSpeed, 1 - age / duration);
        if (age < inputBuffer * duration)
            bufferPoint = true;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.rb.velocity = currentSpeed * dashDir;
    }
}
#endregion
