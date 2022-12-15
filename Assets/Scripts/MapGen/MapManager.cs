using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using veci2 = UnityEngine.Vector2Int;
namespace ProcGen
{
    [RequireComponent(typeof(VoronoiMapGen))]
    public class MapManager : MonoBehaviour
    {
        public static MapManager _;
        public int seed;

        VoronoiMapGen mapGen;
        TreeGen treeGen;
        public static Region GetRegion(Vector2 loc) => VoronoiMapGen._.GetRegion(loc);

        private void Awake()
        {
            _ = this;
        }
        private void Start()
        {
            Generate();
        }
        public void Generate()
        {
            mapGen = GetComponent<VoronoiMapGen>();
            treeGen = GetComponent<TreeGen>();
            seed = Random.Range(0, int.MaxValue);
            mapGen.seed = seed;
            mapGen.GenerateMap();
            treeGen.SpawnTrees();

        }
    }
}