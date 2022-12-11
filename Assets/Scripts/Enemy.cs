using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum Element{FIRE, WATTER, EARTH, WIND}  //Enemy Archetypes, not sure what we doing with it yet
    public int HP;
    public Element element;
    

    public Enemy(){
        HP = 100;
        this.element = Element.FIRE;
        //Debug.Log("Default enemy created");
    }

    public Enemy(int HP, Element element){
        this.HP = HP;
        this.element = element;
        //Debug.Log("Overloaded eenmy created");
    }
}
