using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class testingTransform : MonoBehaviour
{
    [SerializeField] private Transform pathPoint; 
    public Transform target;
    private int pathIndex = 0;

    private void Start()
    {
        target = LevelManager.main.path[pathIndex];   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) {
            Transform newT = Instantiate(pathPoint);
            LevelManager.main.ClearPath();
            LevelManager.main.AddPathPoint(newT);
            Debug.Log("should clear path");
        }
    }
}
