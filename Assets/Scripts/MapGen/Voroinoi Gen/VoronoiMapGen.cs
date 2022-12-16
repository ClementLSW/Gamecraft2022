using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using veci2 = UnityEngine.Vector2Int;
namespace ProcGen
{
    public class VoronoiMapGen : MonoBehaviour
    {
        public static VoronoiMapGen _;
        public Dictionary<veci2, Region> tiles = new();
        public SpriteRenderer overlay;
        public int biomeGrid;
        [Tooltip("Wiggly")]
        public float noiseMult;
        [Tooltip("Jagged")]
        public float noiseDist;
        public int seed;
        public Region[] regions = new Region[0];
        public Biome[] biomes;
        public RectInt mapSize;
        public bool autoUpdate = false;
        public bool isGened = false;
        [Tooltip("Region blending (1 is no blending)")]
        [Range(0, 1)]
        public float biomeBlendScalar = 1f;
        private void Awake()
        {
            _ = this;
        }
        public Region GetRegion(Vector2 loc)
        {
            var mapScalar = new Vector2(overlay.transform.localScale.x, overlay.transform.localScale.y) / mapSize.size;
            var newLoc = loc * mapScalar;
            var tileLoc = new veci2(Mathf.RoundToInt(newLoc.x), Mathf.RoundToInt(newLoc.y));
            tiles.TryGetValue(tileLoc, out Region foundReg);
            return foundReg;
        }
        public void GenerateMap()
        {
            tiles.Clear();
            //int[,] biomesMap = new int[mapSize.width, mapSize.height];
            Color[] biomesColorMap = new Color[mapSize.width * mapSize.height];

            System.Random prng = new System.Random(seed);
            SeedRandom.SetSeed(seed);

            int xS = prng.Next(10, 20);
            int yS = prng.Next(10, 20);

            float offsetX = prng.Next(-100000, 100000);
            float offsetY = prng.Next(-100000, 100000);
            Dictionary<veci2, Region> regionGrid = new();


            for (int gridX = 0; gridX < mapSize.width / biomeGrid; gridX++)
            {
                for (int gridY = 0; gridY < mapSize.height / biomeGrid; gridY++)
                {
                    var gridVec = new veci2(gridX, gridY);
                    //This algo of voronoi generation will make center always be in top right or bottom left quadrants of the gridCell
                    int centerScalar = SeedRandom.Get(gridX, gridY) % biomeGrid;
                    veci2 center = new veci2(centerScalar + gridX * biomeGrid, centerScalar + gridY * biomeGrid);
                    Biome selectedBiome = biomes[math.abs( SeedRandom.Get(gridX, -gridY)) % biomes.Length];
                    var region = new Region() { biome = selectedBiome, Center = center };
                    regionGrid.Add(gridVec, region);
                }
            }

            for (int x = 0; x < mapSize.width; x++)
            {
                for (int y = 0; y < mapSize.height; y++)
                {
                    int gridX = (int)Mathf.Floor(x / biomeGrid);
                    int gridY = (int)Mathf.Floor(y / biomeGrid);
                    ////This algo of voronoi generation will make center always be in top right or bottom left quadrants of the gridCell
                    //int centerScalar = SeedRandom.Get(gridX, gridY) % biomeGrid;
                    //veci2 center = new veci2(centerScalar + gridX * biomeGrid, centerScalar + gridY * biomeGrid);

                    //int x1 = gridX * biomeGrid - mapSize.xMin;
                    //int y1 = gridY * biomeGrid - mapSize.yMin;
                    //Debug.DrawLine(new Vector2(x1, 0), new Vector2(x1, 100000), Color.blue, 50f);
                    //Debug.DrawLine(new Vector2(0, y1), new Vector2(100000, y1), Color.blue, 50f);
                    //int loc = SeedRandom.Get(gridX, gridY) % biomeGrid;
                    //KongrooUtils.DrawDebugCircle(new Vector2(loc + x1, loc + y1), 1f, Color.black, 10, 50f);



                    if (x / biomeGrid - gridX > 0.5f)
                        gridX -= 2;
                    else
                        gridX -= 1;

                    if (y / biomeGrid - gridY > 0.5f)
                        gridY -= 2;
                    else
                        gridY -= 1;


                    int closest = 0;
                    int closestDist = int.MaxValue;
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {

                            #region Voronoi
                            int curBiome = i * 4 + j;
                            int biomeX = SeedRandom.Get(gridX + i, gridY + j) % biomeGrid;
                            int biomeY = SeedRandom.Get(gridX + i, gridY + j) % biomeGrid;

                            int dist = ((gridX + i) * biomeGrid + biomeX - x) * ((gridX + i) * biomeGrid + biomeX - x) +
                                       ((gridY + j) * biomeGrid + biomeY - y) * ((gridY + j) * biomeGrid + biomeY - y);
                            #endregion

                            #region Perlin
                            dist += (int)(Mathf.PerlinNoise(noiseDist * ((gridX + i) * biomeGrid + biomeX - x + offsetX) / 100f,
                                                             noiseDist * ((gridY + j) * biomeGrid + biomeY - y + offsetY) / 100f) * noiseMult);
                            #endregion
                            if (dist < closestDist)
                            {
                                closestDist = dist;
                                closest = curBiome;
                            }
                        }
                    }

                    //biomesMap[x, y] = SeedRandom.Get(gridX + closest / 4, gridY + closest % 4) % biomes.Length;
                    //biomesColorMap[y * mapSize.height + x] = biomes[biomesMap[x, y]].colour;
                    var selectedGridCell = new veci2(gridX + closest / 4, gridY + closest % 4);
                    if (regionGrid.TryGetValue(selectedGridCell, out var selectedRegion))
                    {
                        biomesColorMap[y * mapSize.height + x] = selectedRegion.biome.colour;
                        tiles.Add(new veci2(x, y), selectedRegion);
                    }

                }
            }

            regions = regionGrid.Values.ToArray();
            isGened = true;
            var imageTex = new Texture2D(mapSize.width, mapSize.height);
            imageTex.filterMode = FilterMode.Bilinear;
            imageTex.wrapMode = TextureWrapMode.Clamp;
            imageTex.SetPixels(biomesColorMap);
            imageTex.Apply();

            var newTex = Resize(imageTex, (int)(mapSize.width * biomeBlendScalar), (int)(mapSize.height * biomeBlendScalar));

            //map.material.SetTexture("_MainTex", imageTex);

            var mat = new MaterialPropertyBlock();
            mat.SetTexture("_MainTex", newTex);
            overlay.SetPropertyBlock(mat);
        }

        Texture2D Resize(Texture2D texture2D, int targetX, int targetY)
        {
            RenderTexture rt = new RenderTexture(targetX, targetY, 24);
            RenderTexture.active = rt;
            Graphics.Blit(texture2D, rt);
            Texture2D result = new Texture2D(targetX, targetY);
            result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
            result.Apply();
            return result;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            foreach (var region in regions)
            {
                Gizmos.DrawWireSphere((Vector2)region.Center, 1);
            }
        }
        //for (int x = 0; x < mapSize.width; x++)
        //{
        //    for (int y = 0; y < mapSize.height; y++)
        //    {
        //        int gridX = (int)Mathf.Floor(x / biomeGrid);
        //        int gridY = (int)Mathf.Floor(y / biomeGrid);
        //        int centerScalar = SeedRandom.Get(gridX, gridY) % biomeGrid;
        //        veci2 center = new veci2(centerScalar + gridX * biomeGrid, centerScalar + gridY * biomeGrid);
        //        int x1 = gridX * biomeGrid - mapSize.xMin;
        //        int y1 = gridY * biomeGrid - mapSize.yMin;

        //        Gizmos.color = Color.blue;
        //        Gizmos.DrawLine(new Vector2(x1, 0), new Vector2(x1, 100000));
        //        Gizmos.DrawLine(new Vector2(0, y1), new Vector2(100000, y1));
        //    }
        //}
    }
}
