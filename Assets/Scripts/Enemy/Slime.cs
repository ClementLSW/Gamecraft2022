using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    [Header("Components")]
    public GameObject Player;       // Can probably change datatype back to transform
    private Rigidbody2D rb;
    public EnemyStateManager esm;

    void Awake(){
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        esm.Update();
        
        if(this.HP <= 0){
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        esm.OnCollision(col);
    }    
}
