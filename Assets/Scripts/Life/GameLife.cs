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
                Tilemap.SetTile(new Vector3Int(x, y, 0), Dead);
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
                _gameMap = UpdateLife();

                _currentTime = 0f;
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Vector3Int position = Tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if (position.y < height && 
                    position.x < width &&
                    position.y >= 0 &&
                    position.x >= 0)
                    if (Tilemap.GetTile(position) == Dead)
                    {
                        Tilemap.SetTile(position, Alive);
                        _gameMap[position.x, position.y] = 1;
                    }
                    else
                    {
                        Tilemap.SetTile(position, Dead);
                        _gameMap[position.x, position.y] = 0;
                    }
            }
        }
    }

    int[,] UpdateLife()
    {
        int[,] newMap = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int count = CountNeighbours(x, y, _gameMap);

                if (count == 3)
                {
                    newMap[x, y] = 1;
                    Tilemap.SetTile(new Vector3Int(x, y, 0), Alive);
                }
                else if (count == 2 && _gameMap[x, y] == 1)
                {
                    newMap[x, y] = 1;
                    Tilemap.SetTile(new Vector3Int(x, y, 0), Alive);
                }
                else
                {
                    newMap[x, y] = 0;
                    Tilemap.SetTile(new Vector3Int(x, y, 0), Dead);
                }
            }
        }

        return newMap;
    }

    int CountNeighbours(int x, int y, int[,] checkMap)
    {
        int count = 0;
        for (int sx = x-1; sx <= x+1; sx++)
        {
            for (int sy = y - 1; sy <= y + 1; sy++)
            {
                if (sx >= 0 && sy >= 0 && sx < width && sy < height)
                    if (sx != x || sy != y)
                        if (checkMap[sx, sy] == 1)
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
