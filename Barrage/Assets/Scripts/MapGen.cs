using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapGen : MonoBehaviour
{
    [SerializeField] private int mapWidth, mapHeight;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Sprite emptyTile, downLeft, leftRight, leftUp, rightDown, upDown, upRight;

    private int curX;
    private int curY;
    private Sprite spriteToUse;
    private bool forceDirectionChange = false;

    private bool continueLeft = false;
    private bool continueRight = false;
    private int currentCount = 0;

    //watch this video for path gen
    //https://www.youtube.com/watch?v=P1iIxS8hlhI&ab_channel=Garnet 

    //watch this video for ui elements
    //https://www.youtube.com/watch?v=3g7BtlBCRj0&list=PLfX6C2dxVyLz_w9AWxvkRKc2zUvBl0GIl&index=8&ab_channel=MuddyWolf
}
