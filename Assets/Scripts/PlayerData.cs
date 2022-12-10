using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Preferences
{
    public float musicVol;
    public float soundVol;
}

public class PlayerData : MonoBehaviour
{
    public static PlayerData i;
    public Preferences defaultPrefs;
    public Preferences prefs;
    string prefsPath;
    private void Awake()
    {
        if (i == null)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
            RunOnce();
        }
        else
            Destroy(gameObject);
    }
    void RunOnce()
    {
        prefsPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "UserSettings.json";
        prefs = LoadPrefs();
        if (prefs == null)
            prefs = defaultPrefs;
    }
    public async void SavePrefs()
    {
        Debug.Log("Saving user prefs at " + prefsPath);
        string json = JsonUtility.ToJson(prefs);

        using StreamWriter writer = new StreamWriter(prefsPath);
        await writer.WriteAsync(json);
    }
    public Preferences LoadPrefs()
    {
        try
        {
            using StreamReader reader = new StreamReader(prefsPath);
            string json = reader.ReadToEnd();
            return JsonUtility.FromJson<Preferences>(json);
        }
        catch
        {
            return null;
        }
    }
}
