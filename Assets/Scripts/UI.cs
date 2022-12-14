using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI _;
    public Text ammoCount;
    public Slider cooldownSlider;
    public Text cooldownCount;
    public GridLayoutGroup chargesGrid;
    public Image chargeIconPrefab;
    List<Image> chargeIcons = new();
    private void Awake()
    {
        _ = this;
    }
    private void Update()
    {
        UpdateAmmo();
        UpdateCooldown();
    }
    public void InitCharges(int totalCharges)
    {
        chargeIcons.Clear();
        for (int i = 0; i < totalCharges; i++)
            chargeIcons.Add(Instantiate(chargeIconPrefab, chargesGrid.transform));
    }
    void UpdateAmmo()
    {
        ammoCount.text = $"{GameManager.Player.primary.currentAmmo}/{GameManager.Player.primary.maxAmmo}";
    }
    void UpdateCooldown()
    {
        bool showCooldown = GameManager.Player.secondary.currentCharges < GameManager.Player.secondary.charges;
        bool showCharges = GameManager.Player.secondary.charges > 1;
        cooldownSlider.gameObject.SetActive(showCooldown);
        chargesGrid.gameObject.SetActive(showCharges);

        cooldownSlider.value = Mathf.Clamp01(GameManager.Player.secondary.currentCooldown / GameManager.Player.secondary.cooldown);
        cooldownCount.text = Mathf.CeilToInt(GameManager.Player.secondary.currentCooldown).ToString();
        for (int i = 0; i < GameManager.Player.secondary.charges; i++)
            chargeIcons[i].gameObject.SetActive(i < GameManager.Player.secondary.currentCharges);
    }
}
