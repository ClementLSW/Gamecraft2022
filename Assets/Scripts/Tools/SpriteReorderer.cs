using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteReorderer : MonoBehaviour
{
    public SpriteRenderer sr;
    private void Awake()
    {
        if (sr == null)
            sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        // Make sure sprite is sorted on bottom
        sr.sortingOrder = Mathf.FloorToInt(transform.position.y * -100);
    }
}
