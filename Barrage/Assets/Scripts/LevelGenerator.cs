using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private int mapWidth, mapHeight;

    [SerializeField] private GameObject tileReference;
    [SerializeField] private Sprite emptyTile, downPath, leftRight, leftDown, rightDown, downLeft, downRight;

    [SerializeField] private Transform pathPointPrefab;
    private List<Transform> path = new List<Transform>();

    private int curX;
    private int curY;
    private Sprite spriteToUse;
    private bool forceDirectionChange = false;

    private bool continueLeft = false;
    private bool continueRight = false;
    private int currentCount = 0;

    private enum CurrentDirection
    {
        LEFT,
        RIGHT,
        DOWN,
        UP
    };
    private CurrentDirection curDirection = CurrentDirection.DOWN;

    public struct TileData
    {
        public Transform transform;
        public SpriteRenderer spriteRenderer;
        public int tileID;
    }

    TileData[,] tileData;

    void Awake()
    {
        tileData = new TileData[mapWidth, mapHeight];
        GenerateMap();
    }

    void GenerateMap()
    {
        for (int x = mapWidth - 1; x >= 0; x--)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                GameObject newTile = Instantiate(tileReference, new Vector2(x*1f, y*1f), Quaternion.identity);
                tileData[x, y].spriteRenderer = newTile.GetComponent<SpriteRenderer>();
                tileData[x, y].tileID = 0;
                tileData[x, y].spriteRenderer.sprite = emptyTile;
                tileData[x, y].transform = newTile.transform;
            }
        }
        StartCoroutine(GeneratePath());
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        RegenerateMap();
    //    }
    //}

    void RegenerateMap()
    {
        StopAllCoroutines();
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                tileData[x, y].spriteRenderer.sprite = emptyTile;
                tileData[x, y].tileID = 0;
                tileData[x, y].transform.position = new Vector2(x, y);
            }
        }
        StartCoroutine(GeneratePath());
    }

    IEnumerator GeneratePath()
    {
        curX = Random.Range(0, mapWidth);
        curY = 0;

        Transform firstPoint = Instantiate(pathPointPrefab);
        firstPoint.position = new Vector3(curX, curY - 1, 0);
        path.Add(firstPoint);

        spriteToUse = downPath;

        while (curY <= mapHeight - 1)
        {
            CheckCurrentDirections();
            ChooseDirection();

            if (curY <= mapHeight - 1)
            {
                UpdateMap(curX, curY, spriteToUse);
            }

            if (curDirection == CurrentDirection.DOWN)
            {
                curY++;
            }

            yield return new WaitForSeconds(0.05f);
        }

        LevelManager.main.SetPath(path);
    }

    private void CheckCurrentDirections()
    {
        if (curDirection == CurrentDirection.LEFT && curX - 1 >= 0 && tileData[curX - 1, curY].tileID == 0)
        {
            curX--;
        }
        else if (curDirection == CurrentDirection.RIGHT && curX + 1 <= mapWidth - 1 && tileData[curX + 1, curY].tileID == 0)
        {
            curX++;
        }
        else if (curDirection == CurrentDirection.UP && curY - 1 >= 0 && tileData[curX, curY - 1].tileID == 0)
        {
            if (continueLeft && tileData[curX - 1, curY - 1].tileID == 0 ||
            continueRight && tileData[curX + 1, curY - 1].tileID == 0)
            {
                curY--;
            }
            else
            {
                forceDirectionChange = true;
                tileData[curX, curY].transform.position = new Vector2(tileData[curX, curY].transform.position.x, tileData[curX, curY].transform.position.y);
            }
        }
        else if (curDirection != CurrentDirection.DOWN)
        {
            forceDirectionChange = true;
            tileData[curX, curY].transform.position = new Vector2(tileData[curX, curY].transform.position.x, tileData[curX, curY].transform.position.y);
        }
    }

    private void ChooseDirection()
    {
        if (currentCount < 3 && !forceDirectionChange)
        {
            currentCount++;
        }
        else
        {
            bool chanceToChange = Mathf.FloorToInt(Random.value * 1.99f) == 0;

            if (chanceToChange || forceDirectionChange || currentCount > 7)
            {
                currentCount = 0;
                forceDirectionChange = false;
                ChangeDirection();
            }

            currentCount++;
        }
    }

    private void ChangeDirection()
    {
        int dirValue = Mathf.FloorToInt(Random.value * 2.99f);

        if (dirValue == 0 && curDirection == CurrentDirection.LEFT && curX - 1 > 0
        || dirValue == 0 && curDirection == CurrentDirection.RIGHT && curX + 1 < mapWidth - 1)
        {
            if (curY - 1 >= 0)
            {
                if (tileData[curX, curY - 1].tileID == 0 &&
                tileData[curX - 1, curY - 1].tileID == 0 &&
                tileData[curX + 1, curY - 1].tileID == 0)
                {
                    GoUp();
                    return;
                }
            }
        }

        if (curDirection == CurrentDirection.LEFT)
        {
            UpdateMap(curX, curY, leftDown);
        }
        else if (curDirection == CurrentDirection.RIGHT)
        {
            UpdateMap(curX, curY, rightDown);
        }

        if (curDirection == CurrentDirection.LEFT || curDirection == CurrentDirection.RIGHT)
        {
            curY++;
            spriteToUse = downPath;
            curDirection = CurrentDirection.DOWN;
            return;
        }

        if (curX - 1 > 0 && curX + 1 < mapWidth - 1 || continueLeft || continueRight)
        {
            if (dirValue == 1 && !continueRight || continueLeft)
            {
                if (tileData[curX - 1, curY].tileID == 0)
                {
                    if (continueLeft)
                    {
                        spriteToUse = rightDown;
                        continueLeft = false;
                    }
                    else
                    {
                        spriteToUse = downLeft;
                    }
                    curDirection = CurrentDirection.LEFT;
                }
            }
            else
            {
                if (tileData[curX + 1, curY].tileID == 0)
                {
                    if (continueRight)
                    {
                        continueRight = false;
                        spriteToUse = leftDown;
                    }
                    else
                    {
                        spriteToUse = downRight;
                    }
                    curDirection = CurrentDirection.RIGHT;
                }
            }
        }
        else if (curX - 1 > 0)
        {
            spriteToUse = downLeft;
            curDirection = CurrentDirection.LEFT;
        }
        else if (curX + 1 < mapWidth - 1)
        {
            spriteToUse = downRight;
            curDirection = CurrentDirection.RIGHT;
        }

        if (curDirection == CurrentDirection.LEFT)
        {
            GoLeft();
        }
        else if (curDirection == CurrentDirection.RIGHT)
        {
            GoRight();
        }
    }


    private void GoUp()
    {
        if (curDirection == CurrentDirection.LEFT)
        {
            UpdateMap(curX, curY, downRight);
            continueLeft = true;
        }
        else
        {
            UpdateMap(curX, curY, downLeft);
            continueRight = true;
        }
        curDirection = CurrentDirection.UP;
        curY--;
        spriteToUse = downPath;
    }

    private void GoLeft()
    {
        UpdateMap(curX, curY, spriteToUse);
        curX--;
        spriteToUse = leftRight;
    }

    private void GoRight()
    {
        UpdateMap(curX, curY, spriteToUse);
        curX++;
        spriteToUse = leftRight;
    }

    private void UpdateMap(int mapX, int mapY, Sprite spriteToUse)
    {
        tileData[mapX, mapY].transform.position = new Vector2(tileData[mapX, mapY].transform.position.x, tileData[mapX, mapY].transform.position.y);
        tileData[mapX, mapY].tileID = 1;
        tileData[mapX, mapY].spriteRenderer.sprite = spriteToUse;

        Transform newPathPoint = Instantiate(pathPointPrefab);
        newPathPoint.position = new Vector3(mapX, mapY, 0);
        path.Add(newPathPoint);
    }

}