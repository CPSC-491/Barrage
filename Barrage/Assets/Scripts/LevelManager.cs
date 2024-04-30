using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public List<Transform> path = new List<Transform>();

    public int money;


    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        money = 100;
    }

    public void IncreaseMoney(int amount) 
    { 
        money += amount;
    }

    public bool SpendMoney(int amount)
    {
        if (money >= amount) 
        {
            money -= amount;
            return true;
        } else
        {
            Debug.Log("Not enough Money");
            return false;
        }
    }

    public void AddPathPoint(Transform newPathPoint)
    {
        path.Add(newPathPoint);
    }

    public void ClearPath()
    {
        path.Clear();
    }

    public void SetPath(List<Transform> newPath)
    {
        path = newPath;
        startPoint = path[0];
    }
}
