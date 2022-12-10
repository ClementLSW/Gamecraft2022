using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : StateMachine
{
    protected internal Rigidbody2D rb;
    protected internal SpriteRenderer sr;
    Camera cam;
    protected internal Vector2 moveDir;
    protected internal Vector2 lookDir;
    [Header("Components")]
    public PlayerProjectile attackPrefab;
    public Slider reloadBar;
    public override BaseState DefaultState() => new StopState(this);
    [Header("Player Stats")]
    public float moveSpeed = 4f;
    public float attackMoveSpeedMultiplier = 0.75f;
    public int maxAmmo = 6;
    protected internal int currentAmmo;
    public float attackRate = 0.5f;
    public float reloadDur = 0.75f;
    public float projectileSpeed = 7f;
    public float projectileRange = 10f;
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;
    }
    protected override void Update()
    {
        base.Update();
        Vector2 target = cam.ScreenToWorldPoint(Input.mousePosition);
        lookDir = (target - (Vector2)transform.position).normalized;
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDir = moveInput.magnitude > 1e-7 ? moveInput.normalized : Vector2.zero;

        if (currentState is not BaseActionState && Input.GetKey(KeyCode.Mouse0))
            ChangeState(new ShootState(this));
        if (currentState is not ReloadState &&
            ((Input.GetKey(KeyCode.Mouse0) && currentAmmo <= 0) ||
            (Input.GetKeyDown(KeyCode.Mouse1) && currentAmmo < maxAmmo)))
            ChangeState(new ReloadState(this));
    }
    public void ShootProjectile()
    {
        currentAmmo--;
        var attack = Instantiate(attackPrefab, transform.position, Quaternion.identity).GetComponent<PlayerProjectile>();
        attack.direction = lookDir;
        attack.range = projectileRange;
        attack.speed = projectileSpeed;
    }
}
