using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2; //hitpoints

    public void TakeDmg(int dmg) 
    {
        hitPoints -= dmg;

        if (hitPoints <= 0) 
        {
            EnemySpawner.onEnemyDestroyed.Invoke();
            Destroy(gameObject);
        }
    }
}
