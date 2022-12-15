using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : StateMachine
{
    public int xp;
    public Element element;
    internal Vector2 moveDir;
    internal SpriteRenderer sr;
    internal Rigidbody2D rb;
    internal StatusManager status;
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
    }
    protected override void Update()
    {
        base.Update();
        moveDir = (GameManager.Player.transform.position - transform.position).normalized;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        rb.velocity = GameManager.TimeScale * base.moveSpeed * moveDir;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("PlayerPrimary"))
        {
            // We wanna avoid getcomponents for performance so we can get access from the cached player stats
            health -= GameManager.Player.primary.baseDamage;
        }
        // Just use new tags for each unique type of player attack
        if (health <= 0)
            Despawn();
    }
    public void Despawn()
    {
        //TODO: Return back to object pool instead of destroying, also spawn death effects in a deathstate instead of deleting instantly
        XpPooler.i.SpawnXp(xp, transform.position, element);
        Destroy(gameObject);
    }
}
