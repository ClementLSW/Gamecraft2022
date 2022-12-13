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
        public Region[] regions = new Region[0];

        VoronoiMapGen mapGen;
        public static Region GetRegion(Vector2 loc) => VoronoiMapGen._.GetRegion(loc);

        private void Awake()
        {
            _ = this;
            mapGen = GetComponent<VoronoiMapGen>();
        }
        private void Start()
        {
            seed = Random.Range(0, int.MaxValue);
            mapGen.seed = seed;
            mapGen.GenerateMap();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            foreach (var region in regions)
            {
                Gizmos.DrawWireSphere((Vector2)region.Center, 1);
            }
        }
    }
}