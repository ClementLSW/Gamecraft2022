using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : StateMachine
{
    public int totalHealth;
    public float moveSpeed;
    public int xp;
    public Element element;
    internal Vector2 moveDir;
    internal SpriteRenderer sr;
    internal Rigidbody2D rb;
    internal StatusManager status;
    internal int currentHealth;
    protected override void Awake()
    {
        base.Awake();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        status = GetComponent<StatusManager>();
    }
    protected override void Start()
    {
        base.Start();
        Affinity affinity = AssetDB._.elementAffinity[element];
        sr.color = affinity.colourProfile;
        currentHealth = totalHealth;
        status.currentStatus.Clear();
    }
    private void OnEnable()
    {
        StatusDB._.StatusTick += OnStatusTick;
    }
    private void OnDisable()
    {
        StatusDB._.StatusTick -= OnStatusTick;
    }
    protected override void Update()
    {
        base.Update();
        moveDir = (GameManager.Player.transform.position - transform.position).normalized;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        rb.velocity = GameManager.TimeScale * moveSpeed * moveDir;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("PlayerPrimary"))
        {
            // We wanna avoid getcomponents for performance so we can get access from the cached player stats
            totalHealth -= GameManager.Player.primary.baseDamage;
        }
        // Just use new tags for each unique type of player attack
        if (totalHealth <= 0)
            Despawn();
    }
    public void Despawn()
    {
        //TODO: Return back to object pool instead of destroying, also spawn death effects in a deathstate instead of deleting instantly
        XpPooler.i.SpawnXp(xp, transform.position, element);
        Destroy(gameObject);
    }
    public void OnStatusTick()
    {
        if (status.HasStatus(StatusDB._.burn))
        {
        }
    }
}
