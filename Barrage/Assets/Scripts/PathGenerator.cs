using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator
{
    private List<GameObject> path = new List<GameObject>();
    private List<GameObject> topTiles = new List<GameObject>();
    private List<GameObject> bottomTiles = new List<GameObject>();

    private int radius;
    private int currentTileIndex;

    private bool atLastX;
    private bool atLastY;

    private GameObject startTile;
    private GameObject endTile;

    public List<GameObject> GetPath()
    {
        return path;
    }
    public PathGenerator(int radius)
    {
        this.radius = radius;
    }

    public void AssignTopAndBottomTiles(int y, GameObject tile)
    {
        if (y == 0) topTiles.Add(tile);
        if (y == radius - 1) bottomTiles.Add(tile);
    }

    private bool AssignAndCheckStartEndTile()
    {
        int xIndex = Random.Range(0, topTiles.Count - 1);
        int yIndex = Random.Range(0, bottomTiles.Count - 1);

        startTile = topTiles[xIndex];
        endTile = bottomTiles[yIndex];

        return startTile != null && endTile != null;
    }

    public void GeneratePath()
    {
        if(AssignAndCheckStartEndTile())
        {
            GameObject currentTile = startTile;

            for (int i = 0; i < 3; i++)
            {
                MoveRight(ref currentTile);
            }
            
            var safetybrake = 0;
            while (!atLastX)
            {
                safetybrake++;
                if (safetybrake > 100) break;

                if (currentTile.transform.position.x > endTile.transform.position.x)
                {
                    MoveDown(ref currentTile);
                } else if (currentTile.transform.position.x < endTile.transform.position.x)
                {
                    MoveUp(ref currentTile);
                } else
                {
                    atLastX = true;
                }
            }

            safetybrake = 0;
            while (!atLastY)
            {
                safetybrake++;
                if(safetybrake > 100) break;

                if (currentTile.transform.position.y > endTile.transform.position.y)
                {
                    MoveRight(ref currentTile);
                } else if(currentTile.transform.position.y < endTile.transform.position.y)
                {
                    MoveLeft(ref currentTile);
                } else
                {
                    atLastY = true;
                }
            }

            path.Add(endTile);
        }
    }

    private void MoveDown(ref GameObject currentTile)
    {
        path.Add(currentTile);
        currentTileIndex = WorldGenerator.GeneratedTiles.IndexOf(currentTile);
        int n = currentTileIndex - radius;
        currentTile = WorldGenerator.GeneratedTiles[n];
    }

    private void MoveUp(ref GameObject currentTile)
    {
        path.Add(currentTile);
        currentTileIndex = WorldGenerator.GeneratedTiles.IndexOf(currentTile);
        int n = currentTileIndex + radius;
        currentTile = WorldGenerator.GeneratedTiles[n];
    }

    private void MoveLeft(ref GameObject currentTile)
    {
        path.Add(currentTile);
        currentTileIndex = WorldGenerator.GeneratedTiles.IndexOf(currentTile);
        currentTileIndex++;
        currentTile = WorldGenerator.GeneratedTiles[currentTileIndex];
    }

    private void MoveRight(ref GameObject currentTile)
    {
        path.Add(currentTile);
        currentTileIndex = WorldGenerator.GeneratedTiles.IndexOf(currentTile);
        currentTileIndex--;
        currentTile = WorldGenerator.GeneratedTiles[currentTileIndex];
    }
}
