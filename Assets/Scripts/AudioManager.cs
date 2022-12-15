using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager i;
    public AudioMixer mixer;
    public AudioSource musicSource, soundSource;
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
    }
    void RunOnce()
    {
        SetMusicVol(AssetDB._.prefs.musicVol);
        SetSoundVol(AssetDB._.prefs.soundVol);
    }
    public void SetMusicVol(float value)
    {
        float linear2dB = value == 0 ? -80f : 20f * Mathf.Log10(value);
        mixer.SetFloat("musicVol", linear2dB);
    }
    public void SetSoundVol(float value)
    {
        float linear2dB = value == 0 ? -80f : 20f * Mathf.Log10(value);
        mixer.SetFloat("soundVol", linear2dB);
    }

    public void PlaySFX(AudioClip clip){
        soundSource.clip = clip;
        soundSource.Play();
    }

    public void PlayBGM(AudioClip clip, bool loop = true){
        musicSource.loop = loop;
        musicSource.clip = clip;
        musicSource.Play();
    }
}
