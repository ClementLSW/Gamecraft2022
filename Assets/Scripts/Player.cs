using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine
{
    protected internal Rigidbody2D rb;
    protected internal SpriteRenderer sr;
    public override BaseState DefaultState() => new IdleState(this);
    public Vector2 moveDir;
    public Vector2 lookDir;
    Camera cam;
    [Header("Player Stats")]
    public float moveSpeed = 5f;
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
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        rb.velocity = moveSpeed * moveDir;
    }
}
