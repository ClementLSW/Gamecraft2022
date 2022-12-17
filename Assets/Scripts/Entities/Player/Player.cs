using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerStates;
using System.Linq;

enum AnimState { IdleStop, IdleMove, AttackStop, AttackMove, Special }
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
    public float shockwaveProc = 0; // Proc chance of shockwave
    public float shockWaveRatio = 0; // damage and knockback conversion ratio when enemies are knocked into each other
    public float frostbiteProc = 0; // frozen once reaches 1
    public float frostbiteDamageRatio = 0.25f; // percentage health damage
    public float slowStrength = 0;
    public float frostDur = 0;
    [Header("Player Progression")]
    public uint[] xpProgress = new uint[4]; // For current level up bar only
    public uint[] xpTotal = new uint[4];
    #region Animations
    internal AnimState currentAnim;
    public static readonly int IdleStopKey = Animator.StringToHash("IdleStop");
    public static readonly int IdleMoveKey = Animator.StringToHash("IdleMove");
    public static readonly int AttackStopKey = Animator.StringToHash("AttackStop");
    public static readonly int AttackMoveKey = Animator.StringToHash("AttackMove");
    public static readonly int SpecialKey = Animator.StringToHash("Special");
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
        primary = Instantiate(startingWeapon, transform);
        primary.Init(this);
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
        CalculateXp();
        //Region currentReg = ProcGen.MapManager.GetRegion(transform.position);
        //if (currentReg == null) return;
        //print($"Currently in {currentReg.biome.name}");
    }
    internal void AnimateSprites(AnimState nextAnim, bool flipX) // Ew enums non ideal implementation but oh well
    {
        sr.flipX = flipX;
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
            case AnimState.Special:
                anim.PlayInFixedTime(SpecialKey);
                break;
        }
        currentAnim = nextAnim;
    }
    public void TakeDamage()
    {

    }
    #region Progression
    void CalculateXp() // Unsure if gpu to cpu calculation should be done in update loop, also, assigning a array copy as a ref type in xpPooler?
    {
        //if (GameManager.IsPaused) return;
        uint totalProgress = 0;
        for (int i = 0; i < xpTotal.Length; i++)
        {
            if (xpTotal[i] < XpPooler.collectedXp[i])
            {
                xpProgress[i] += XpPooler.collectedXp[i] - xpTotal[i];
                xpTotal[i] = XpPooler.collectedXp[i];
                // Do ui lerping shit
            }
            totalProgress += xpProgress[i];
        }
        if (totalProgress >= GameManager.NeededToLevel)
            LevelUp();
    }
    public void LevelUp()
    {
        GameManager.OnLevelUp();
        for (int i = 0; i < xpProgress.Length; i++)
            xpProgress[i] = 0;
    }
    public void GetUpgrade(Upgrade upgrade)
    {
        if (!upgrades.Contains(upgrade))
        {
            upgrade.OnAcquire(this);
            upgrades.Add(upgrade);
        }
    }
    #endregion
}
