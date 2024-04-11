using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    [SerializeField] private int mapWidth, mapHeight;
    [SerializeField] private GameObject tilePrefab;

    private void Awake()
    {
        GenerateMap();
    }
    void GenerateMap()
    {
        for (int x = -8; x <= mapWidth; x++)
        {
            for (int y = -5; y <= mapHeight; y++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(x,y, 0), Quaternion.identity);
            }
        }
    }
}
