using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
namespace ProcGen
{
    public class TreeGen : MonoBehaviour
    {
        public float radius = 1;
        public Vector2 regionSize = Vector2.one;
        public int rejectionSamples = 30;
        public float displayRadius = 1;
        public Tree treePrefab;
        List<Vector2> points;

        public void SpawnTrees()
        {
            points = PoissonDiscSampling.GeneratePoints(radius, regionSize, rejectionSamples);
            for (int i = 0; i < points.Count; i++)
            {
                var region = MapManager.GetRegion(points[i]);
                if (region == null) continue;
                if (region.biome.treeSprites.Length > 0)
                {
                    Tree tree = Instantiate(treePrefab, points[i], Quaternion.identity);
                    tree.sr.sprite = region.biome.treeSprites.RandomElement();
                }
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(regionSize / 2, regionSize);
            Gizmos.color = Color.white;
            if (points != null)
            {
                foreach (Vector2 point in points)
                {
                    Gizmos.DrawWireSphere(point, displayRadius);
                }
            }
        }
    }
}

