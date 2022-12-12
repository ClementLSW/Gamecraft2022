using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (VoronoiMapGen))]
public class VoronoiMapGeneratorEditor : Editor {

	public override void OnInspectorGUI() {
        VoronoiMapGen mapGen = (VoronoiMapGen)target;

		if (DrawDefaultInspector ()) {
			if (mapGen.autoUpdate) {
				mapGen.GenerateMap ();
			}
		}

		if (GUILayout.Button ("Generate")) {
			mapGen.GenerateMap ();
		}
	}
}
