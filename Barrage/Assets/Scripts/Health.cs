using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2; //hitpoints

    private bool isDestroyed = false;
    public void TakeDmg(int dmg) 
    {
        hitPoints -= dmg;

        if (hitPoints <= 0 && !isDestroyed) 
        {
            EnemySpawner.onEnemyDestroyed.Invoke();
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
