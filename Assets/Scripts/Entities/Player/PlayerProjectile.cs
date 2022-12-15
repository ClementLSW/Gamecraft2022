using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
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
    }
    private void Update()
    {
        age += Time.deltaTime * speed; // distance = time * speed
        if (age > range) // despawn when proj reaches distance limit
            Despawn();
    }
    private void FixedUpdate()
    {
        rb.velocity = speed * targetDir;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pierce <= 0 && collision.CompareTag("Mob"))
            Despawn();
        else
            pierce--;
    }
    public void Despawn()
    {
        Destroy(gameObject);
    }
}
