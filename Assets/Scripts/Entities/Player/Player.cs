using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerStates;

public class Player : StateMachine
{
    internal Rigidbody2D rb;
    internal SpriteRenderer sr;
    internal Vector2 moveDir;
    internal Vector2 lookDir;
    internal PrimaryWeapon primary = null;
    internal SecondaryAbility secondary = null;
    internal HashSet<Upgrade> upgrades = new();
    Camera cam;
    [Header("Components")]
    public Slider reloadBar;
    public PrimaryWeapon startingWeapon;
    public override BaseState DefaultState() => new StopState(this);
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

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
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
        Region currentReg = ProcGen.MapManager.GetRegion(transform.position);
        if (currentReg == null) return;
        print($"Currently in {currentReg.biome.name}");
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
