﻿using UnityEngine;
using System.Collections;
using UnityEditor;
using ProcGen;

[CustomEditor(typeof(VoronoiMapGen))]
public class VoronoiMapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        VoronoiMapGen mapGen = (VoronoiMapGen)target;

        if (DrawDefaultInspector())
        {
            if (mapGen.autoUpdate)
            {
                mapGen.GenerateMap();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            mapGen.GenerateMap();
        }
    }
}


[CustomEditor(typeof(MapManager))]
public class MapManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapManager mapGen = (MapManager)target;

        if (DrawDefaultInspector())
        {
        }

        if (GUILayout.Button("Generate"))
        {
            mapGen.Generate();
        }
    }
}

