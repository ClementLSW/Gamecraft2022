using System.Collections;
using System.Collections.Generic;
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
    public static bool IsPaused { get => instance._isPaused; set { instance._isPaused = value; } }


    public AudioClip bgm;

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

        AudioManager.i.PlayBGM(bgm);
    }
    void RunOnce()
    {

    }
}
