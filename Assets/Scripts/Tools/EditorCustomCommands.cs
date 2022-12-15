using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCustomCommands : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Zawarudo.StartZaWarudo();
        }
        if (Input.GetKeyDown(KeyCode.X))
            Zawarudo.EndZaWarudo();
        if (Input.GetKeyDown(KeyCode.Alpha9))
            GameManager.Player.GetUpgrade(UpgradeDB._.tailwind);
        if (Input.GetKeyDown(KeyCode.Alpha1))
            GameManager.Player.GetUpgrade(UpgradeDB._.pyromancy5);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            GameManager.Player.GetUpgrade(UpgradeDB._.pyromaniac5);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            GameManager.Player.GetUpgrade(UpgradeDB._.multishot5);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            GameManager.Player.GetUpgrade(UpgradeDB._.swiftshot5);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            GameManager.Player.GetUpgrade(UpgradeDB._.stagger5);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            GameManager.Player.GetUpgrade(UpgradeDB._.shockwave5);
        if (Input.GetKeyDown(KeyCode.Alpha7))
            GameManager.Player.GetUpgrade(UpgradeDB._.pierce5);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            GameManager.Player.GetUpgrade(UpgradeDB._.frost5);
#endif
    }
}
