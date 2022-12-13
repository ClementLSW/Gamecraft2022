using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Math = System.Math;

public class Room
{
    public Vector2Int Center { get; private set; } = Vector2Int.zero;
    public uint LifeLines { get; set; } = 1;
    public List<Vector2Int> tiles { get; private set; } = new();
    public Vector2Int CurrentLoc { get; private set; } = Vector2Int.zero;
    public Color color;
    public Room(Vector2Int _center, uint _lifeLines)
    {
        Center = _center;
        CurrentLoc = Center;
        LifeLines = _lifeLines;
        color = Color.HSVToRGB(Random.value, 0.4f, 0.6f);
    }

    public void Travel(Vector2Int cardinal)
    {
        tiles.Add(CurrentLoc);
        CurrentLoc += cardinal;
    }
    public bool isInRegion(Vector2Int input) => tiles.Any(t => t == input);

    public void Collided()
    {
        LifeLines = Math.Clamp(LifeLines - 1, 0, uint.MaxValue);
        CurrentLoc = Center;
    }

}

public class WormGen : MonoBehaviour
{
    public int roomsToGen = 4;
    public List<Room> rooms = new();
    public RectInt mapSize;
    public MeshRenderer map;
    public int maxIterations = 20;
    public uint lifeLines = 1;

    public static readonly Vector2Int[] cardinal =
    {
        Vector2Int.down,
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.left,
    };

    private void Start()
    {
        // TODO use better algorithm lmao
        for (int i = 0; i < roomsToGen; i++)
        {
            var randx = Random.Range(mapSize.xMin, mapSize.xMax);
            var randy = Random.Range(mapSize.yMin, mapSize.yMax);
            rooms.Add(new Room(new Vector2Int(randx, randy), lifeLines));
        }

        int currIter = 0;
        while (!rooms.All(r => r.LifeLines == 0) && currIter < maxIterations)
        {
            foreach (var r in rooms)
            {
                if (r.LifeLines == 0) continue;
                var dir = cardinal[Random.Range(0, cardinal.Length)];
                r.Travel(dir);
                bool isCollided = false;
                if (!mapSize.Contains( r.CurrentLoc))
                {
                    isCollided = true;
                }
                else
                {
                    //foreach (var compare in rooms)
                    //{
                    //    if (compare == r) continue;
                    //    if (compare.isInRegion(r.CurrentLoc))
                    //    {
                    //        isCollided = true;
                    //        break;
                    //    }
                    //}
                }
                if (isCollided) r.Collided();
            }
            currIter++;
        }

        //PaintRooms
        var imageTex = new Texture2D(mapSize.width, mapSize.height);
        imageTex.filterMode = FilterMode.Point;
        imageTex.wrapMode = TextureWrapMode.Clamp;
        var colorArr = new Color[mapSize.width * mapSize.height];
        foreach (var r in rooms)
            foreach (var t in r.tiles)
            {
                int coord2Index = t.x * mapSize.width + t.y;
                colorArr[coord2Index] = r.color;
            }
        imageTex.SetPixels(colorArr);
        imageTex.Apply();
        map.material.SetTexture("_MainTex", imageTex);
    }
}
