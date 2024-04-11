using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public static List<GameObject> GeneratedTiles = new List<GameObject>();
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private int radius = 10;
    void Start()
    {
        PathGenerator pg = new PathGenerator(radius); //pg = path generator

        for (int x = 0; x < radius; x++)
        {
            for (int y = 0; y < radius; y++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(x * 1f, y * 1f), Quaternion.identity);

                GeneratedTiles.Add(tile);
                pg.AssignTopAndBottomTiles(y, tile);
            }
        }

        pg.GeneratePath();

        foreach(var pObject in pg.GetPath())
        {
            pObject.SetActive(false);
        }
    }
}
