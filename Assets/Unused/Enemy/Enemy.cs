using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour{

    public enum Element{FIRE, WATER, EARTH, WIND};

    [Header("Stats")]
    public int HP = 100;
    public float range;
    public float speed;
    public float detectRange;
    public Element element;
    
    public Enemy(){
        HP = 100;
        //Debug.Log("Default enemy created");
    }

    public void TakeDamage(int dmg){
        this.HP -= dmg;
    }

}
