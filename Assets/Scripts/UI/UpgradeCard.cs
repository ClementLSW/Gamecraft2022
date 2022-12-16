using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    Button btn;
    public Upgrade upgrade;
    public Image background;
    public Image icon;
    public Text title;
    public Text description;
    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => ChooseUpgrade());
    }
    public void InitCard(Upgrade toInit)
    {
        upgrade = toInit;
        background.color = AssetDB._.elementCol[upgrade.element].uiTheme;
        icon.sprite = upgrade.icon;
        title.text = upgrade.name;
        description.text = upgrade.description;
        title.color = AssetDB._.elementCol[upgrade.element].lightTheme;
        description.color = AssetDB._.elementCol[upgrade.element].lightTheme;
    }
    public void ChooseUpgrade()
    {
        GameManager.OnSelectUpgrade(upgrade);
    }
}
