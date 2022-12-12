using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoronoiNoise : MonoBehaviour
{
    public static int[,] GenerateBiomeMap(int mapWidth, int mapHeight, int seed, int biomesGrid, int biomesNum, float noiseMult, float noiseDist)
    {
        int[,] biomesMap = new int[mapWidth, mapHeight];

        System.Random prng = new System.Random(seed);
        SeedRandom.SetSeed(seed);

        int xS = prng.Next(10, 20);
        int yS = prng.Next(10, 20);

        float offsetX = prng.Next(-100000, 100000);
        float offsetY = prng.Next(-100000, 100000);

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                int gridX = (int)Mathf.Floor(x / biomesGrid);
                int gridY = (int)Mathf.Floor(y / biomesGrid);

                if (x / biomesGrid - gridX > 0.5f)
                    gridX -= 2;
                else
                    gridX -= 1;

                if (y / biomesGrid - gridY > 0.5f)
                    gridY -= 2;
                else
                    gridY -= 1;

                int closest = 0;
                int closestDist = int.MaxValue;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        int curBiome = i * 4 + j;
                        int biomeX = SeedRandom.Get(gridX + i, gridY + j) % biomesGrid;
                        int biomeY = SeedRandom.Get(gridX + i, gridY + j) % biomesGrid;

                        int dist = ((gridX + i) * biomesGrid + biomeX - x) * ((gridX + i) * biomesGrid + biomeX - x) +
                                   ((gridY + j) * biomesGrid + biomeY - y) * ((gridY + j) * biomesGrid + biomeY - y);

                        dist += (int)(Mathf.PerlinNoise(noiseDist * ((gridX + i) * biomesGrid + biomeX - x + offsetX) / 100f,
                                                         noiseDist * ((gridY + j) * biomesGrid + biomeY - y + offsetY) / 100f) * noiseMult);

                        if (dist < closestDist)
                        {
                            closestDist = dist;
                            closest = curBiome;
                        }
                    }
                }

                biomesMap[x, y] = SeedRandom.Get(gridX + closest / 4, gridY + closest % 4) % biomesNum;

            }
        }

        return biomesMap;
    }
}
