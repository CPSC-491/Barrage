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
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
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
