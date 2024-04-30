using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int moneyValue = 50;

    public HealthBar healthBar;

    private bool isDestroyed = false;
    public void TakeDmg(int dmg) 
    {
        hitPoints -= dmg;
        healthBar.SetHealth(hitPoints);
        if (hitPoints <= 0 && !isDestroyed) 
        {
            EnemySpawner.onEnemyDestroyed.Invoke();
            LevelManager.main.IncreaseMoney(moneyValue);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        healthBar.SetMaxHealth(hitPoints);
    }
}
