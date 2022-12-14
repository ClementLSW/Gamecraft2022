using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 targetDir;
    public float speed;
    public float range;
    public bool piercing;
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
            Despawn();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!piercing && collision.CompareTag("Mob"))
            Despawn();
    }
    public void Despawn()
    {
        Destroy(gameObject);
    }
}
