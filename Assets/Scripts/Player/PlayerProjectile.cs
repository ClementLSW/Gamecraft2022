using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 direction;
    public float speed;
    public float range;
    float age;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        rb.velocity = speed * direction;
    }
    private void Update()
    {
        age += Time.deltaTime * speed; // distance = time * speed
        if (age > range) // despawn when proj reaches distance limit
            Despawn();
    }
    public void Despawn()
    {
        Destroy(gameObject);
    }
}
