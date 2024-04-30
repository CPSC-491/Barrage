using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int playerHP = 100;
    public HealthBar healthBar;

    public bool isHPZero = false;

    private void Start()
    {
        healthBar.SetMaxHealth(playerHP);
    }

    public void PlayerTakeDamage(int dmg)
    {
        playerHP -= dmg;
        if (playerHP <= 0)
        {
            healthBar.SetHealth(0);
            Debug.Log("Player has died");
            Time.timeScale = 0f; //pause game
            Debug.Log("canvas should appear saying player has died");
        }
        else
        {
            healthBar.SetHealth(playerHP);
        }

    }
}
