using ProcGen;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EnemyStates;

public class BaseEnemy : StateMachine
{
    public int xp;
    public Element element;
    internal Vector2 moveDir;
    internal Animator anim;
    internal ParticleSystem ps;
    internal CircleCollider2D circleCollider;
    internal SpriteRenderer[] sr;
    int[] initialSpriteOrder;
    internal Rigidbody2D rb;
    internal StatusManager status;
    public AudioClip hitSound;
    public Material spriteMat;
    public Color staggerColor = Color.red;
    public override BaseState DefaultState() => new MoveState(this);
    public static readonly int IdleKey = Animator.StringToHash("idle");
    public static readonly int MoveKey = Animator.StringToHash("move");
    public static readonly int AttackKey = Animator.StringToHash("attack");

    bool flipBody;
    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        ps = GetComponent<ParticleSystem>();
        sr = GetComponentsInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        status = GetComponent<StatusManager>();
        spriteMat = AssetDB._.enemyMat;
        foreach (var s in sr)
        {

            s.material = spriteMat;
            s.color = Color.black;
        }
        initialSpriteOrder = sr.Select(s => s.sortingOrder).ToArray();

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
        ReorderSprites();

    }
    void ReorderSprites()
    {
        for (int i = 0; i < sr.Length; i++)
        {
            SpriteRenderer s = sr[i];
            s.sortingOrder = Mathf.FloorToInt(transform.position.y * -100 + initialSpriteOrder[i]);
        }

    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("PlayerPrimary"))
        {
            // We wanna avoid getcomponents for performance so we can get access from the cached player stats
            health -= Mathf.CeilToInt(GameManager.Player.primary.baseDamage * GameManager.Player.primary.damageScale);
            var vel = collision.attachedRigidbody.velocity;
            var knockback = GameManager.Player.primary.knockback;
            ChangeState(new StaggerState(this, 0.25f + knockback, vel, knockback * 1 / circleCollider.radius));
            if (GameManager.Player.shockwaveProc > Random.value) status.AddStatus(StatusType.Shockwave, new StatusInfo());
            if (GameManager.Player.frostDur > 0) status.AddStatus(StatusType.Frost, new StatusInfo() { timer = GameManager.Player.frostDur });
            if (GameManager.Player.frostbiteProc > 0) status.AddStatus(StatusType.Frostbite, new StatusInfo() { buildup = GameManager.Player.frostbiteProc });
            if (GameManager.Player.burnProc > Random.value) status.AddStatus(StatusType.Burn, new StatusInfo() { timer = GameManager.Player.burnDur });

        }
        // Just use new tags for each unique type of player attack
        if (health <= 0)
            Despawn();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mob") && currentState is StaggerState staggerState && status.HasStatus(StatusType.Shockwave))
        {
            var enemy = collision.gameObject.GetComponent<BaseEnemy>();
            if (enemy.status.HasStatus(StatusType.Shockwave)) return;
            enemy.ChangeState(new StaggerState(enemy, staggerState.StaggerDist * GameManager.Player.shockWaveRatio, rb.velocity, staggerState.StaggerDist * 1 / circleCollider.radius * GameManager.Player.shockWaveRatio));
            enemy.status.AddStatus(StatusType.Shockwave, new StatusInfo());
        }

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
