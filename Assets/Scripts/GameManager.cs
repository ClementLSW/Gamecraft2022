using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Player _player;
    private float _tickRate = 0.4f; // Tickrate for DoT and status effects
    private float _timeScale = 1; // Player stuff are not affected but everything else should be
    private bool _isPaused = false;
    public static Player Player { get => instance._player; }
    public static void SetPlayer(Player player) { instance._player = player; }
    public static float TickRate { get => instance._tickRate; }
    public static float TimeScale { get => instance._timeScale; }
    public static void SetTimeScale(float timeScale) { instance._timeScale = timeScale; }
    public static bool IsPaused
    {
        get => instance._isPaused;
        set
        {
            instance._isPaused = value;
            Time.timeScale = value ? 0 : 1;
        }
    }

    public AudioClip bgm;
    public static Color StaggerMatColour = new Color(6, 1, 1, 1);

    [Header("Progression")]
    public int levelUpOptions = 4;
    public int baseXpScaling = 100;
    public float xpScaling = 1.15f;
    #region Meth
    public static int BaseXpScaling { get => instance.baseXpScaling; }
    public static float XpScaling { get => instance.xpScaling; }
    public static int LevelUpOptions { get => instance.levelUpOptions; }
    public static int CurrentLevel { get => Player.upgrades.Count + 1; }
    //public static uint NeededToLevel => LevelToXp(CurrentLevel + 1) - LevelToXp(CurrentLevel);
    public static uint NeededToLevel => LevelToXp(CurrentLevel + 1);
    public static uint LevelToXp(int level)
    {
        float constantK = Mathf.Log(BaseXpScaling, XpScaling) - 1;
        uint xp = (uint)Mathf.Floor(Mathf.Pow(XpScaling, level + constantK) - BaseXpScaling);
        return xp;
    }
    #endregion
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            RunOnce();
        }
        else
            Destroy(gameObject);

        //AudioManager.i.PlayBGM(bgm);
    }
    public static void OnLevelUp()
    {
        //var possibleOptions = UpgradeDB.Upgrades.Where(s => !Player.upgrades.Contains(s) && s.upgradeRequirements.Length == Player.upgrades.Intersect(s.upgradeRequirements).Count());
        var currentlyUnobtained = UpgradeDB.Upgrades.Except(Player.upgrades).ToDebuggableList();
        var possibleOptions = currentlyUnobtained.Where(s => s.upgradeRequirements.Length == Player.upgrades.Intersect(s.upgradeRequirements).Count());

        if (possibleOptions.ToArray().Length == 0) return;

        // Weighted shuffle algo
        for (int i = 0; i < Player.xpProgress.Length; i++)
        {

        }

        var shuffled = KongrooUtils.ShuffleArray(possibleOptions.ToArray());
        var rolledOptions = shuffled.Take(LevelUpOptions);

        UI._.levelupScreen.SetActive(true);
        UI._.InitLevelUp(rolledOptions.ToArray());
        IsPaused = true;
    }
    public static void OnSelectUpgrade(Upgrade selected)
    {
        Player.GetUpgrade(selected);
        UI._.levelupScreen.SetActive(false);
        IsPaused = false;
    }
    void RunOnce()
    {

    }
}
