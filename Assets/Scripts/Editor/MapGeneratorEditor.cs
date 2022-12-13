using UnityEngine;
using System.Collections;
using UnityEditor;


//public interface IGeneratable
//{
//	public bool autoUpdate { get; set; }
//	public void Generate();
//}

[CustomEditor (typeof (MapGenerator))]
public class MapGeneratorEditor : Editor {

	public override void OnInspectorGUI() {
		MapGenerator mapGen = (MapGenerator)target;

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
