using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager i;
    public AudioClip bgm;
    private void Awake()
    {
        if (i == null)
        {
            i = this;
            DontDestroyOnLoad(i);
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
