using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class BasePlayerState : BaseState
{
    protected readonly Player player;
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
public class BaseIdleState : BasePlayerState
{
    public BaseIdleState(Player sm) : base(sm) { }
}
public class StopState : BaseIdleState
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
public class MoveState : BaseIdleState
{
    public MoveState(Player sm) : base(sm) { }
    public override void Update()
    {
        base.Update();
        if (player.moveDir.magnitude == 0)
            sm.ChangeState(new StopState(player));
    }
}
public class BaseActionState : BasePlayerState
{
    public BaseActionState(Player sm) : base(sm) { }
}
public class ShootState : BaseActionState
{
    const float shotBuffer = 0.1f;
    float exitTimer;
    public ShootState(Player sm) : base(sm) { }
    public override void OnEnter()
    {
        base.OnEnter();
        age = shotBuffer;
        exitTimer = player.attackRate;
    }
    public override void Update()
    {
        base.Update();
        age -= Time.deltaTime;
        if (age <= 0 && player.currentAmmo > 0)
        {
            player.ShootProjectile();
            age = player.attackRate;
        }
        if (!Input.GetKey(KeyCode.Mouse0))
            exitTimer -= Time.deltaTime;
        else
            exitTimer = player.attackRate;
        if (exitTimer <= 0 || player.currentAmmo <= 0)
            sm.ChangeState(sm.DefaultState());
    }
    public override void FixedUpdate()
    {
        player.rb.velocity = player.moveSpeed * player.attackMoveSpeedMultiplier * player.moveDir;
    }
}
public class ReloadState : BaseActionState
{
    public ReloadState(Player sm) : base(sm)
    {
        duration = player.reloadDur;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        player.reloadBar.gameObject.SetActive(true);
    }
    public override void Update()
    {
        base.Update();
        player.reloadBar.value = 1 - age / duration;
    }
    public override void OnExit()
    {
        base.OnExit();
        player.reloadBar.gameObject.SetActive(false);
        if (age <= 0)
            player.currentAmmo = player.maxAmmo;
    }
}
