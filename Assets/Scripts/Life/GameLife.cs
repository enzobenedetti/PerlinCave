using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameLife : MonoBehaviour
{
    public int width;
    public int height;
    private int[,] _gameMap;
    public Tilemap Tilemap;
    private bool simulationOn;

    public float updateSpeed;
    private float _currentTime;

    [Header("Tiles")]
    public Tile Dead;
    public Tile Alive;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _gameMap = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tilemap.SetTile(new Vector3Int(x, y, 0), Random.Range(0f, 1f) < 0.3f ? Alive : Dead);
                if (Tilemap.GetTile(new Vector3Int(x, y, 0)) == Alive)
                    _gameMap[x, y] = 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (simulationOn)
        {
            _currentTime += Time.deltaTime;
            if (updateSpeed <= _currentTime)
            {
                UpdateLife();

                _currentTime = 0f;
            }
        }
    }

    void UpdateLife()
    {
        int[,] copyMap = _gameMap;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int count = CountNeighbours(x, y, copyMap);

                if (count == 3)
                {
                    _gameMap[x, y] = 1;
                    Tilemap.SetTile(new Vector3Int(x, y, 0), Alive);
                }
                else if (count == 2 && copyMap[x, y] == 1)
                {
                    _gameMap[x, y] = 1;
                    Tilemap.SetTile(new Vector3Int(x, y, 0), Alive);
                }
                else
                {
                    _gameMap[x, y] = 0;
                    Tilemap.SetTile(new Vector3Int(x, y, 0), Dead);
                }
            }
        }
    }

    int CountNeighbours(int x, int y, int[,] copyMap)
    {
        int count = 0;
        for (int sx = x-1; sx <= x+1; sx++)
        {
            for (int sy = y - 1; sy <= y + 1; sy++)
            {
                if (sx >= 0 && sy >= 0 && sx < width && sy < height)
                    if (sx != x || sy != y)
                        if (copyMap[sx, sy] == 1)
                            count++;
                if (count > 3)
                    return count;
            }
        }

        return count;
    }

    public void StartSimulation()
    {
        simulationOn = true;
    }
}
