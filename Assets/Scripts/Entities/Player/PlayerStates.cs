using UnityEngine;

namespace PlayerStates
{
    // This script is gonna break 2000 lines hehe
    public class BasePlayerState : BaseState
    {
        protected readonly Player player;
        protected readonly float inputBufferRatio = 0.5f;
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
    public class IdleState : BaseIdle
    {
        bool standingDir;
        public IdleState(Player sm) : base(sm) { }
        public override void Update()
        {
            base.Update();
            if ((standingDir && player.moveDir.x > 0) || (!standingDir && player.moveDir.x < 0))
                standingDir = !standingDir;
            player.AnimateSprites(player.moveDir == Vector2.zero ? AnimState.IdleStop : AnimState.IdleMove, player.moveDir == Vector2.zero ? player.lookDir.x < 0 : standingDir);
        }
    }
    #endregion
    #region Primary Attack States
    public class BasePrimary : BasePlayerState
    {
        protected readonly PrimaryWeapon weapon;
        public BasePrimary(Player sm) : base(sm)
        {
            weapon = player.primary;
        }
        public override void Update()
        {
            base.Update();
            player.AnimateSprites(player.moveDir == Vector2.zero ? AnimState.AttackStop : AnimState.AttackMove, player.lookDir.x < 0);
        }
    }
    public class ShootState : BasePrimary
    {
        public ShootState(Player sm) : base(sm)
        {
            duration = weapon.fireRate;
            bufferPoint = false;
        }
        public override void OnEnter()
        {
            base.OnEnter();
            weapon.currentAmmo--;
            for (int i = 0; i < weapon.projectiles; i++)
            {
                float angle = weapon.spreadAngle == 0 ? 0 : KongrooUtils.RemapRange(i, 0, weapon.projectiles - 1, -weapon.spreadAngle / 2, weapon.spreadAngle / 2);
                weapon.ActivatePrimary(player.lookDir.Vector2Rotate(angle));
            }
        }
        public override void Update()
        {
            base.Update();
            if (age < inputBufferRatio * duration)
                bufferPoint = true;
        }
        public override void FixedUpdate()
        {
            player.rb.velocity = player.moveSpeed * player.primary.attackMoveSpeedMultiplier * player.moveDir;
        }
    }
    #endregion
    #region Secondary Attack States
    public class BaseSecondary : BasePlayerState
    {
        public BaseSecondary(Player sm) : base(sm) { }
        public override void OnEnter()
        {
            base.OnEnter();
            player.AnimateSprites(AnimState.Special, player.lookDir.x < 0);
        }
    }
    public class WindDashState : BaseSecondary
    {
        Vector2 dashDir;
        float currentSpeed;
        protected readonly WindDashAbility ability;
        public WindDashState(Player sm) : base(sm)
        {
            ability = sm.secondary as WindDashAbility;
            duration = ability.dashDur;
            bufferPoint = false;
        }
        public override void OnEnter()
        {
            base.OnEnter();
            ability.ActivateSecondary();
            //dashDir = player.moveDir == Vector2.zero ? player.lookDir : player.moveDir;
            dashDir = player.lookDir;
        }
        public override void Update()
        {
            base.Update();
            currentSpeed = Mathf.SmoothStep(ability.dashSpeed + player.moveSpeed, player.moveSpeed, 1 - age / duration);
            if (age < inputBufferRatio * duration)
                bufferPoint = true;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            player.rb.velocity = currentSpeed * dashDir;
        }
    }
    public class FireShootState : BaseSecondary
    {
        public FireShootState(Player sm) : base(sm)
        {

        }
    }
    public class EarthRollState : BaseSecondary
    {
        public EarthRollState(Player sm) : base(sm)
        {

        }
    }
    public class WaterBlastState : BaseSecondary
    {
        public WaterBlastState(Player sm) : base(sm)
        {

        }
    }

    #endregion
    public class StaggerState : BasePlayerState
    {
        Vector2 knockbackDir;
        float currentSpeed;
        public StaggerState(Player sm, Vector2 dir) : base(sm)
        {
            duration = 0.25f;
            knockbackDir = dir;
        }
        public override void OnEnter()
        {
            base.OnEnter();
            player.health--;
            player.bufferedState = null;
            player.AnimateSprites(AnimState.Special, knockbackDir.x < 0);
            player.invincibilityTimer = 2;
            //AudioManager.i.PlaySFX(player.hitSound);
        }
        public override void Update()
        {
            base.Update();
            currentSpeed = Mathf.SmoothStep(20, 0, 1 - age / duration);
            if (age <= duration * 0.5f)
                bufferPoint = true;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            player.rb.velocity = currentSpeed * knockbackDir;
        }
    }
}