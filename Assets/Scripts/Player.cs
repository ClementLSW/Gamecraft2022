using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    //public Transform targetLookAt;
    //public float lookAheadDist = 10f;
    //bool toggleLookAhead = false;
    public override BaseState DefaultState() => new StopState(this);
    internal virtual BasePrimary PrimaryAttack() => new WindShot(this);
    internal virtual BaseSecondary SecondaryAttack() => new WindDash(this);
    protected internal bool FirePrimary => Input.GetMouseButton(0) && currentAmmo > 0;
    protected internal bool FireSecondary => Input.GetMouseButton(1);
    [Header("Player Stats")]
    public float moveSpeed = 4f;
    public float attackMoveSpeedMultiplier = 0.75f;
    public int maxAmmo = 6;
    protected internal int currentAmmo;
    public float primaryRate = 0.5f;
    public float reloadDur = 0.75f;
    float reloadTimer;
    public float projectileSpeed = 7f;
    public float projectileRange = 10f;
    public float secondaryRate = 0.5f;
    public float dashSpeed = 7f;
    //TODO: store primary and secondary ability data in equipment instead of on player
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
        currentAmmo = maxAmmo;
    }
    protected override void Update()
    {
        base.Update();
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        lookDir = (mousePos - (Vector2)transform.position).normalized;
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDir = moveInput.magnitude > 1e-7 ? moveInput.normalized : Vector2.zero;
        ReloadAmmo();

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
    public void ShootProjectile()
    {
        currentAmmo--;
        var attack = Instantiate(attackPrefab, transform.position, Quaternion.identity).GetComponent<PlayerProjectile>();
        attack.direction = lookDir;
        attack.range = projectileRange;
        attack.speed = projectileSpeed;
    }
    public void ReloadAmmo()
    {
        if (currentState is BasePrimary || currentState is BaseSecondary || currentAmmo >= maxAmmo)
        {
            reloadBar.gameObject.SetActive(false);
            reloadTimer = 0;
        }
        else
        {
            reloadBar.gameObject.SetActive(true);
            reloadTimer += Time.deltaTime;
            reloadBar.value = reloadTimer / reloadDur;
            if (reloadTimer >= reloadDur)
            {
                currentAmmo = maxAmmo;
                reloadTimer = 0;
            }
        }
    }
}
