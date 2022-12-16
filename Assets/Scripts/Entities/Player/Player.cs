using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerStates;
enum AnimState { IdleStop, IdleMove, AttackStop, AttackMove }
public class Player : StateMachine
{
    internal Rigidbody2D rb;
    internal Vector2 moveDir;
    internal Vector2 lookDir;
    internal PrimaryWeapon primary = null;
    internal SecondaryAbility secondary = null;
    internal HashSet<Upgrade> upgrades = new();
    Camera cam;
    [Header("Components")]
    public SpriteRenderer sr;
    public Animator anim;
    public Slider reloadBar;
    public PrimaryWeapon startingWeapon;
    public override BaseState DefaultState() => new IdleState(this);
    internal virtual BasePrimary PrimaryAbility() => primary.PrimaryState();
    internal virtual BaseSecondary SecondaryAbility() => secondary.SecondaryState();
    internal bool FirePrimary => Input.GetMouseButton(0) && primary.CanActivate;
    internal bool FireSecondary => Input.GetMouseButton(1) && secondary != null && secondary.CanActivate;
    [Header("Status Effects")]
    public float burnDamageRatio = 1; // burn DoT on burned enemies, percentage of base damage
    public float burnProc = 0; // burn chance
    public float burnDur = 5f;
    public float shockwaveProc = 0; // damage and knockback conversion ratio when enemies are knocked into each other
    public float frostbiteProc = 0; // frozen once reaches 1
    public float frostbiteDamageRatio = 0.25f; // percentage health damage
    public float slowStrength = 0;
    public float frostDur = 0;
    #region Animations
    AnimState currentAnim;
    public static readonly int IdleStopKey = Animator.StringToHash("IdleStop");
    public static readonly int IdleMoveKey = Animator.StringToHash("IdleMove");
    public static readonly int AttackStopKey = Animator.StringToHash("AttackStop");
    public static readonly int AttackMoveKey = Animator.StringToHash("AttackMove");
    #endregion
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }
    protected override void Start()
    {
        base.Start();
        GameManager.SetPlayer(this);
        // Init abilities on pickup
        primary = Instantiate(startingWeapon, transform);
        primary.Init(this);
        //secondary.Init(this);
    }
    protected override void Update()
    {
        base.Update();
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        lookDir = (mousePos - (Vector2)transform.position).normalized;
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDir = moveInput.magnitude > 1e-7 ? moveInput.normalized : Vector2.zero;

        primary.ReloadAmmo();
        if (secondary)
            secondary.RechargeCooldown();

        // State management
        if (currentState is BaseIdle && FirePrimary)
            ChangeState(PrimaryAbility());
        if (currentState is not BaseSecondary && FireSecondary)
            ChangeState(SecondaryAbility());
        if (currentState.bufferPoint)
        {
            if (FirePrimary)
                bufferedState = PrimaryAbility();
            if (FireSecondary)
                bufferedState = SecondaryAbility();
        }

        // Anim management
        if (currentState is BaseIdle)
        {
            sr.flipX = moveDir == Vector2.zero ? lookDir.x < 0 : moveDir.x < 0;
            AnimateSprites(moveDir == Vector2.zero ? AnimState.IdleStop : AnimState.IdleMove);
        }
        else if (currentState is BasePrimary || currentState is BaseSecondary)
        {
            sr.flipX = lookDir.x < 0;
            AnimateSprites(moveDir == Vector2.zero ? AnimState.AttackStop : AnimState.AttackMove);
        }

        //Region currentReg = ProcGen.MapManager.GetRegion(transform.position);
        //if (currentReg == null) return;
        //print($"Currently in {currentReg.biome.name}");
    }
    void AnimateSprites(AnimState nextAnim) // Non ideal implementation but oh well
    {
        if (currentAnim == nextAnim) return;
        switch (nextAnim)
        {
            case AnimState.IdleStop:
                anim.PlayInFixedTime(IdleStopKey);
                break;
            case AnimState.IdleMove:
                anim.PlayInFixedTime(IdleMoveKey);
                break;
            case AnimState.AttackStop:
                anim.PlayInFixedTime(AttackStopKey);
                break;
            case AnimState.AttackMove:
                anim.PlayInFixedTime(AttackMoveKey);
                break;
        }
        currentAnim = nextAnim;
    }
    public void TakeDamage()
    {

    }
    public void GetUpgrade(Upgrade upgrade)
    {
        if (!upgrades.Contains(upgrade))
        {
            upgrade.OnAcquire(this);
            upgrades.Add(upgrade);
        }
    }
}
