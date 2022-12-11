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
    Camera cam;
    [Header("Components")]
    public PlayerProjectile attackPrefab;
    public Slider reloadBar;
    public BasePrimaryAbility primary;
    public BaseSecondaryAbility secondary;
    public override BaseState DefaultState() => new StopState(this);
    internal virtual BasePrimary PrimaryAttack() => primary.PrimaryState();
    internal virtual BaseSecondary SecondaryAttack() => secondary.SecondaryState();
    protected internal bool FirePrimary => Input.GetMouseButton(0) && currentAmmo > 0;
    protected internal bool FireSecondary => Input.GetMouseButton(1) && currentCooldown <= 0;
    [Header("Player Stats")]
    public float moveSpeed = 4f;
    public float attackMoveSpeedMultiplier = 0.75f;
    protected internal int currentAmmo;
    protected internal float currentCooldown;
    float reloadTimer;
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
        InitAbilities();
    }
    protected override void Update()
    {
        base.Update();
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        lookDir = (mousePos - (Vector2)transform.position).normalized;
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDir = moveInput.magnitude > 1e-7 ? moveInput.normalized : Vector2.zero;
        ReloadAmmo();
        RechargeCooldown();

        if (currentState is BaseIdle && FirePrimary)
            ChangeState(PrimaryAttack());
        if (currentState is not BaseSecondary && FireSecondary)
            ChangeState(SecondaryAttack());
        if (currentState.bufferPoint)
        {
            if (FirePrimary)
                bufferedState = PrimaryAttack();
            if (FireSecondary)
                bufferedState = SecondaryAttack();
        }
    }
    public void ActivatePrimary()
    {
        currentAmmo--;
        var attack = Instantiate(attackPrefab, transform.position, Quaternion.identity).GetComponent<PlayerProjectile>();
        attack.direction = lookDir;
        attack.range = primary.projectileRange;
        attack.speed = primary.projectileSpeed;
    }
    public void ActivateSecondary()
    {
        currentCooldown = secondary.cooldown;
    }
    public void ReloadAmmo()
    {
        if (currentState is BasePrimary || currentState is BaseSecondary || currentAmmo >= primary.maxAmmo)
        {
            reloadBar.gameObject.SetActive(false);
            reloadTimer = 0;
        }
        else
        {
            reloadBar.gameObject.SetActive(true);
            reloadTimer += Time.deltaTime;
            reloadBar.value = reloadTimer / primary.reloadDur;
            if (reloadTimer >= primary.reloadDur)
            {
                currentAmmo = primary.maxAmmo;
                reloadTimer = 0;
            }
        }
    }
    public void RechargeCooldown()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
            // Do some ui shit here
        }
    }
    public void InitAbilities()
    {
        primary.player = this;
        secondary.player = this;
        currentAmmo = primary.maxAmmo;
        currentCooldown = 0;
    }
}
