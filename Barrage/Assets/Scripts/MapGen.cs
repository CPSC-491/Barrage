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
    //watch this video for path gen
    //https://www.youtube.com/watch?v=P1iIxS8hlhI&ab_channel=Garnet 

    //watch this video for ui elements
    //https://www.youtube.com/watch?v=3g7BtlBCRj0&list=PLfX6C2dxVyLz_w9AWxvkRKc2zUvBl0GIl&index=8&ab_channel=MuddyWolf
}
