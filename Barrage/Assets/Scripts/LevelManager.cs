using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Attributes")]
    public int playerHP = 100;
    public HealthBar healthBar;
    public bool isHPZero = false;

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
        money = 225;
        healthBar.SetMaxHealth(playerHP);
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

    public void PlayerTakeDamage(int dmg)
    {
        playerHP -= dmg;
        if (playerHP <= 0)
        {
            isHPZero = true;
            healthBar.SetHealth(0);
            Debug.Log("Player has died");
            Time.timeScale = 0f; //pause game
            Debug.Log("canvas should appear saying player has died");
        }
        else
        {
            healthBar.SetHealth(playerHP);
            Debug.Log("Current hp is " + playerHP);
        }

    }
}
