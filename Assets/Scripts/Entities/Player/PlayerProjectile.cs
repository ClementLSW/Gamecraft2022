using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : PooledItem
{
    Rigidbody2D rb;
    public Vector2 targetDir;
    public float speed;
    public float range;
    public int pierce;
    float age;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Init()
    {
        rb.velocity = speed * targetDir;
    }
    private void Update()
    {
        age += Time.deltaTime * speed; // distance = time * speed
        if (age > range) // despawn when proj reaches distance limit
            DestroyPooled();
    }
    private void FixedUpdate()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pierce <= 0 && collision.CompareTag("Mob"))
            DestroyPooled();
        else
            pierce--;
    }
    
    protected override void Reset()
    {
        targetDir = Vector2.zero;
        age = 0;
    }
}
