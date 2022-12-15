using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    [Header("Components")]
    public GameObject Player;       // Can probably change datatype back to transform
    private Player player;
    private Rigidbody2D rb;
    public RangedStateManager rsm;

    void setElement(Element e){
        this.element = e;
    }

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.Find("Player");
        player = Player.GetComponent<Player>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        rsm = gameObject.GetComponent<RangedStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        rsm.Update();

        if(this.HP <= 0){
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.layer==6) Debug.Log("Player damaged"); //player.TakeDamage();
    }

    void TakeDamage(int dmg){
        this.HP -= dmg;
    }
}
