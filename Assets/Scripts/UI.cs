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
    [Header("Level Up")]
    public GameObject levelupScreen;
    public GridLayoutGroup upgradesGrid;
    internal UpgradeCard[] upgradeCards;
    private void Awake()
    {
        _ = this;
        upgradeCards = upgradesGrid.GetComponentsInChildren<UpgradeCard>();
        levelupScreen.SetActive(false);
    }
    private void Update()
    {
        UpdateAmmo();
        if (GameManager.Player.secondary)
        {
            UpdateCooldown();
            UpdateSpecial();
        }
    }
    public void InitCharges(int totalCharges, Color color)
    {
        chargeIcons.Clear();
        for (int i = 0; i < totalCharges; i++)
        {
            chargeIcons.Add(Instantiate(chargeIconPrefab, chargesGrid.transform));
            chargeIcons[i].color = color;
        }
    }
    public void InitLevelUp(Upgrade[] options)
    {
        foreach (var item in upgradeCards)
            item.gameObject.SetActive(false);
        for (int i = 0; i < options.Length; i++)
        {
            upgradeCards[i].gameObject.SetActive(true);
            upgradeCards[i].InitCard(options[i]);
        }
    }
    void UpdateAmmo()
    {
        ammoCount.text = $"{GameManager.Player.primary.currentAmmo}/{GameManager.Player.primary.maxAmmo}";
    }
    void UpdateCooldown()
    {
        cooldownSlider.gameObject.SetActive(GameManager.Player.secondary.ShowCooldown);

        cooldownSlider.value = Mathf.Clamp01(GameManager.Player.secondary.currentCooldown / GameManager.Player.secondary.cooldown);
        cooldownCount.text = Mathf.CeilToInt(GameManager.Player.secondary.currentCooldown).ToString();
    }
    void UpdateSpecial()
    {
        chargesGrid.gameObject.SetActive(GameManager.Player.secondary.ShowSpecial(out int current));
        for (int i = 0; i < chargeIcons.Count; i++)
            chargeIcons[i].gameObject.SetActive(i < current);
    }
}
