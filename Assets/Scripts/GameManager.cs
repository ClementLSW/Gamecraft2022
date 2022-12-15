using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Player _player;
    private float _timeScale = 1; // Player stuff are not affected but everything else should be
    private bool _isPaused = false;
    public static Player Player { get { return instance._player; } }
    public static void SetPlayer(Player player) { instance._player = player; }
    public static float TimeScale { get { return instance._timeScale; } }
    public static void SetTimeScale(float timeScale) { instance._timeScale = timeScale; }
    public static bool IsPaused { get { return instance._isPaused; } set { instance._isPaused = value; } }
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
