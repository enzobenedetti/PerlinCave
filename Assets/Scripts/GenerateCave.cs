using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class GenerateCave : MonoBehaviour
{
    public int height = 20;
    public int width = 20;
    public Int32 mapSeed;
    private System.Random _rnd = new System.Random();
    public float zoom;
    public Tile black;
    public Tile white;
    public Tile mousse;
    public Tile redJewel;
    public Tile blueJewel;
    public float groundClamp;
    public float mossClamp;
    public float jewelClamp;
    public Tilemap CaveTilemap;
    public Tilemap MousseTilemap;
    public Tilemap JewelTilemap;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _rnd = new System.Random(mapSeed);
        int groundSeed = _rnd.Next() / 1000;
        int mossSeed = _rnd.Next() / 1000;
        int jewelSeed = _rnd.Next() / 1000;

        CreateTileMap(CaveTilemap, groundSeed);
        CreateTileMap(MousseTilemap, mossSeed);
        CreateTileMap(JewelTilemap, jewelSeed);
    }

    void CreateTileMap(Tilemap tilemap, int seed)
    {
        tilemap.ClearAllTiles();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float tileColor = Mathf.PerlinNoise(seed + x * zoom, seed + y * zoom);
                PaintTile(tilemap, new Vector3Int(x,y,0), tileColor);
            }
        }
    }

    void PaintTile(Tilemap tilemap, Vector3Int localisation, float tileColor)
    {
        if (tilemap == CaveTilemap)
        {
            CaveTilemap.SetTile(localisation, tileColor > groundClamp ? white : black);
        }
        else if (tilemap == MousseTilemap)
        {
            if (tileColor < mossClamp && CaveTilemap.GetTile(localisation) == black)
                MousseTilemap.SetTile(localisation, mousse);
        }
        else if (tilemap == JewelTilemap)
        {
            if (tileColor < jewelClamp && CaveTilemap.GetTile(localisation) == white)
                JewelTilemap.SetTile(localisation, _rnd.Next()%2 == 0 ? blueJewel : redJewel);
        }
    }
}
