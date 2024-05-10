using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Threading.Tasks;

public class LevelManager : MonoBehaviour
{
    [Header("Attributes")]
    public int playerHP = 100;
    public HealthBar healthBar;
    public bool isHPZero = false;
    public int currentWave = 1;

    public static LevelManager main;

    public Transform startPoint;
    public List<Transform> path = new List<Transform>();

    public int money;
    public int maxQuestions = 3;

    [SerializeField] public GameObject gameOverScreen;
    [SerializeField] public GameObject gameWonScreen;


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
            GameOver();
        }
        else
        {
            healthBar.SetHealth(playerHP);
            Debug.Log("Current hp is " + playerHP);
        }
    }

    public void IncreaseWave()
    {
        if (currentWave > 10)
        {
            GameWon();
        }
        currentWave++;
        maxQuestions = 3;
    }

    public void DecreaseQuestion()
    {
        maxQuestions--;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameWon()
    {
        gameWonScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
