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
    Camera cam;
    [Header("Components")]
    public Slider reloadBar;
    public PrimaryWeapon startingWeapon;
    public override BaseState DefaultState() => new StopState(this);
    internal virtual BasePrimary PrimaryAbility() => primary.PrimaryState();
    internal virtual BaseSecondary SecondaryAbility() => secondary.SecondaryState();
    internal bool FirePrimary => Input.GetMouseButton(0) && primary.CanActivate;
    internal bool FireSecondary => Input.GetMouseButton(1) && secondary.CanActivate;
    [Header("Player Stats")]
    public float moveSpeed = 4f;
    public float attackMoveSpeedMultiplier = 0.75f;

    internal HashSet<Upgrade> upgrades = new();
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
        upgrade.OnAcquire(this);
        upgrades.Add(upgrade);
    }
}
