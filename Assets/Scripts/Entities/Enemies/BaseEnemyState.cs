using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates
{
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
    //    Vector2 knockbackDir;
    //    float currentSpeed;

    //    public StaggerState( sm, Vector2 dir) : base(sm)
    //    {
    //        duration = player.baseStaggerDur;
    //        knockbackDir = dir;
    //        SaveParams(dir);
    //    }

    //    public override void OnEnter()
    //    {
    //        base.OnEnter();
    //        player.bufferedState = null;
    //        player.queuedState = null;
    //        player.anim.PlayInFixedTime(Player.StaggerKey);
    //        player.currentEquip.transform.localPosition = Vector2.zero;
    //        player.invincibilityTimer = player.invincibilityDur + duration;
    //        AudioManager.i.PlaySFX(player.hitSound);
    //    }

    //    public override void Update()
    //    {
    //        base.Update();
    //        currentSpeed = Mathf.SmoothStep(player.baseStaggerSpeed, 0, 1 - age / duration);
    //        if (age <= duration * 0.5f)
    //            bufferPoint = true;
    //    }

    //    public override void FixedUpdate()
    //    {
    //        base.FixedUpdate();
    //        player.rb.velocity = currentSpeed * knockbackDir;
    //    }
    //}


    public class StaggerState : BaseEnemyState
    {
        readonly float staggerKnockback;
        float currentStagger;
        float currentSpeed;
        readonly Vector2 knockbackDir;
        public StaggerState(BaseEnemy sm, float staggerDist, Vector2 dir, float staggerDur) : base(sm)
        {
            duration = staggerDur;
            staggerKnockback = staggerDist;
            knockbackDir = dir;
        }
        public override void OnEnter()
        {
            base.OnEnter();
            enemy.anim.PlayInFixedTime(BaseEnemy.IdleKey);
            foreach (var s in enemy.sr)
                s.color = enemy.staggerColor;
            AudioManager.i.PlaySFX(enemy.hitSound);
        }
        public override void Update()
        {
            base.Update();
            currentStagger += Time.deltaTime / (duration);
            currentSpeed = Mathf.SmoothStep(staggerKnockback, 0, currentStagger);
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            enemy.rb.velocity = currentSpeed * knockbackDir;
        }
        public override void OnExit()
        {
            base.OnExit();

            foreach (var s in enemy.sr)
                s.color = Color.black;
        }
    }



    //public class StaggerState : BaseEnemyState
    //{
    //    //TODO: Maybe use enemy.stagger
    //    readonly float baseStaggerDur = 0.25f;
    //    readonly Vector3 knockbackDir;
    //    float currentSpeed;
    //    float currentStagger;

    //    public StaggerState(BaseEnemy sm, Vector3 dir, float staggerDur = 0) : base(sm)
    //    {
    //        duration = baseStaggerDur + staggerDur;
    //        knockbackDir = dir;
    //    }
    //    public override void OnEnter()
    //    {
    //        base.OnEnter();
    //        enemy.anim.PlayInFixedTime(BaseEnemy.IdleKey);
    //        AudioManager.i.PlaySFX(enemy.hitSound);
    //    }
    //    public override void Update()
    //    {
    //        base.Update();
    //        currentStagger += Time.deltaTime / (baseStaggerDur + mob.staggerDur);
    //        currentSpeed = Mathf.SmoothStep(staggerKnockback, 1, currentStagger);
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


}
