using ProcGen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : StateMachine
{
    public int xp;
    public Element element;
    internal Vector2 moveDir;
    internal Animator anim;
    internal CircleCollider2D circleCollider;
    internal SpriteRenderer[] sr;
    internal Rigidbody2D rb;
    internal StatusManager status;
    public override BaseState DefaultState() => new MoveState(this);
    public static readonly int IdleKey = Animator.StringToHash("idle");
    public static readonly int MoveKey = Animator.StringToHash("move");
    public static readonly int AttackKey = Animator.StringToHash("attack");

    bool flipBody;
    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        sr = GetComponentsInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        status = GetComponent<StatusManager>();
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        moveDir = (GameManager.Player.transform.position - transform.position).normalized;
        if (moveDir.x < 0 && flipBody || moveDir.x > 0 && !flipBody)
        {
            flipBody = !flipBody;
            var flipped = transform.localScale;
            flipped.x *= -1;
            transform.localScale = flipped;
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("PlayerPrimary"))
        {
            // We wanna avoid getcomponents for performance so we can get access from the cached player stats
            health -= Mathf.CeilToInt(GameManager.Player.primary.baseDamage * GameManager.Player.primary.damageScale);
        }
        if (collision.CompareTag("Mob") && currentState is StaggerState && status.HasStatus(StatusType.Shockwave))
        {
            collision.GetComponent<BaseEnemy>().status.AddStatus(StatusType.Shockwave, new StatusInfo() { timer = 0.3f });
        }
        // Just use new tags for each unique type of player attack
        if (health <= 0)
            Despawn();
    }
    public void Despawn()
    {
        //TODO: Return back to object pool instead of destroying, also spawn death effects in a deathstate instead of deleting instantly
        XpPooler._.SpawnXp(xp, transform.position, element, circleCollider.radius);
        DestroyPooled();
    }
    protected override void Reset()
    {
    }
    public override void Init()
    {
        base.Init();
        //var region = MapManager.GetRegion(transform.position);
        //element = region.biome.element;
        //sr.color = AssetDB._.elementCol[element].darkTheme;
    }
}