using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    public GameObject Player;       // Can probably change datatype back to transform
    private Rigidbody2D rb;
    private bool dashed = false;
    public float range;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        
        if(this.HP <= 0){
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        switch(col.gameObject.layer){
            case 6:
                //col.gameObject.TakeDamage(10); IDK how you implementing that
                break;
            case 7:
                this.HP -= 10;
                break;
        }
    }    
}
