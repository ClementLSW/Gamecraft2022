using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float lifetime;
    private float age;
    private GameObject Player; 
    private Player player;
    private Rigidbody2D rb;

    void Despawn(){
        Destroy(gameObject);
    }

    void Awake()
    {
        Player = GameObject.Find("Player");
        player = Player.GetComponent<Player>();
        
        rb = gameObject.GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(.5f, .5f);
    }

    void Update()
    {
        if(age >= lifetime){
            Despawn();
        }else{
            age += Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.layer==6){
            //player.TakeDamage();
            Despawn();
        }
    }
}