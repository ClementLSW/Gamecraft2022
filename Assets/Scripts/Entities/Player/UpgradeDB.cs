using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDB : MonoBehaviour
{
    public static UpgradeDB _;
    public Upgrade tailwind;
    private void Awake()
    {
        _ = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
            GameManager.Player.GetUpgrade(tailwind);
    }
}
