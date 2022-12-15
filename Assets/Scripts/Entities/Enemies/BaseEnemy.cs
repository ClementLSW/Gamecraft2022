using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : StateMachine
{
    //public enum Element{FIRE, WATTER, EARTH, WIND}  //Enemy Archetypes, not sure what we doing with it yet
    public int hp;
    public float moveSpeed;
    public int xp;
    public Element element;
    internal Vector2 moveDir;
    internal SpriteRenderer sr;
    internal Rigidbody2D rb;
    protected override void Awake()
    {
        base.Awake();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    protected override void Start()
    {
        base.Start();
        Affinity affinity = AssetDB.i.elementAffinity[element];
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
        rb.velocity = moveSpeed * moveDir;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("PlayerPrimary"))
        {
            // We wanna avoid getcomponents for performance so we can get access from the cached player stats
            hp -= GameManager.Player.primary.damage;
        }
        // Just use new tags for each unique type of player attack
        if (hp <= 0)
            Despawn();
    }
    public void Despawn()
    {
        //TODO: Return back to object pool instead of destroying, also spawn death effects in a deathstate instead of deleting instantly
        XpPooler.i.SpawnXp(xp, transform.position, element);
        Destroy(gameObject);
    }
}
