using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MapGenerator))]
public class MapManager : MonoBehaviour
{
    MapGenerator mapGen;
    private void Awake()
    {
        mapGen = GetComponent<MapGenerator>();
    }
    private void Start()
    {
        mapGen.seed = Random.Range(0, int.MaxValue);
        mapGen.GenerateMap();
    }
}
