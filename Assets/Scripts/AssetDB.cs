using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

<<<<<<< HEAD
public enum Element: uint { Fire, Wind, Earth, Water };
=======
public enum Element: uint { Fire, Wind, Earth, Water, Neutral };
public enum StatusType { Burn, Frost, Frostbite, Shockwave };
>>>>>>> main
public class Region
{
    public Biome biome;
    public Vector2Int Center = Vector2Int.zero;
}
[System.Serializable]
public class Affinity
{
    public float[] baseRatio;
    public Color colourProfile;
}

[System.Serializable]
public class StatusEffect
{
    public bool stackable;
    public bool timed;
}
[System.Serializable]
public class Preferences
{
    public float musicVol;
    public float soundVol;
}
//[ExecuteInEditMode]
public class AssetDB : MonoBehaviour
{
    public static AssetDB _;
    [Header("Game")]
    public SerialKeyValuePair<Element, Affinity>[] elementAffinityInspector;
    public Dictionary<Element, Affinity> elementAffinity = new();
    public SerialKeyValuePair<StatusType, StatusEffect>[] statusTypeInspector;
    public Dictionary<StatusType, StatusEffect> statusType = new();

    [Header("Settings")]
    public Preferences defaultPrefs;
    public Preferences prefs;
    string prefsPath;
    private void Awake()
    {
        if (_ == null)
        {
            _ = this;
            RunOnce();
            DontDestroyOnLoad(gameObject);
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

        elementAffinity = elementAffinityInspector.ToDict();
        statusType = statusTypeInspector.ToDict();
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
    // Im not doing webGL cuz fuck javascript
}
