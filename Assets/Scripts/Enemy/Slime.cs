using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    [Header("Components")]
    public GameObject Player;       // Can probably change datatype back to transform
    private Player player;
    private Rigidbody2D rb;
    public EnemyStateManager esm;

    void setElement(Element e){
        this.element = e;
    }

    void Awake(){
        Player = GameObject.Find("Player");
        player = Player.GetComponent<Player>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        esm.Update();
        
        if(this.HP <= 0){
            Destroy(gameObject);    // I should probably do a dying state
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.layer==6) Debug.Log("Player damaged"); //player.TakeDamage();
    }

    void TakeDamage(int dmg){
        this.HP -= dmg;
    }
}
