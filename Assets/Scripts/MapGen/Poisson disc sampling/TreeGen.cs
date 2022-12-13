using System.Collections;
using System.Collections.Generic;
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

        List<Vector2> points;

        public void SpawnTrees()
        {
            points = PoissonDiscSampling.GeneratePoints(radius, regionSize, rejectionSamples);
            for (int i = 0; i < points.Count; i++)
            {
                var region = MapManager.GetRegion(points[i]);

            }
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(regionSize / 2, regionSize);
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

