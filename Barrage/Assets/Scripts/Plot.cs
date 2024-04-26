using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject troop;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }
    private void OnMouseEnter()
    {
        if(Time.timeScale != 0f)
        {
            sr.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        if (Time.timeScale != 0f) 
        {
            sr.color = startColor;
        }
    }

    private void OnMouseDown()
    {
        if (Time.timeScale != 0f)
        {
            if (troop != null) return;

            TroopTower tempTroop = TroopManager.main.GetSelectedTroop();

            if (tempTroop.cost > LevelManager.main.money)
            {
                Debug.Log("You cant afford this");
                return;
            }

            LevelManager.main.SpendMoney(tempTroop.cost);

            troop = Instantiate(tempTroop.prefab, transform.position, Quaternion.identity);
        }
    }
}
