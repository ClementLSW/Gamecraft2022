using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteReorderer : MonoBehaviour
{
    SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        // Make sure sprite is sorted on bottom
        sr.sortingOrder = Mathf.FloorToInt(transform.position.y * -100);
    }
}
