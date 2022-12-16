using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public static HealthUI _;
    GridLayoutGroup grid;
    Image[] hearts;
    int currentHealth;
    int totalHealth;
    public Color filledCol, emptyCol;
    private void Awake()
    {
        _ = this;
        grid = GetComponent<GridLayoutGroup>();
        hearts = grid.GetComponentsInChildren<Image>();
    }
    private void Update()
    {
        if (currentHealth != GameManager.Player.health)
        {
            currentHealth = GameManager.Player.health;
            int i = 0;
            for (; i < currentHealth; i++)
                hearts[i].color = filledCol;
            for (; i < hearts.Length; i++)
                hearts[i].color = emptyCol;
        }

        if (totalHealth != GameManager.Player.baseHealth)
        {
            totalHealth = GameManager.Player.baseHealth;
            int t = 0;
            for (; t < totalHealth; t++)
                hearts[t].gameObject.SetActive(true);
            for (; t < hearts.Length; t++)
                hearts[t].gameObject.SetActive(false);
        }
    }
}
