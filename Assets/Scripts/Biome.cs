using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Biome.asset", menuName = "MapGen/Biome")]
public class Biome : ScriptableObject
{
    public Element element;
    public Color colour;
    public Gradient elementSpawnRate;
    public Sprite[] treeSprites;
    // mob spawns, spawnrate
}