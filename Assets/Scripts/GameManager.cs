using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Player _player;
    public static Player Player
    {
        get { return instance._player; }
    }
    public static void SetPlayer(Player player)
    {
        instance._player = player;
    }
    public AudioClip bgm;
    private bool isPaused = false;
    public static bool IsPaused
    {
        get { return instance.isPaused; }
        set { instance.isPaused = value; }
    }

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
