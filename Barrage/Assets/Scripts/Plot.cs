using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    public GameObject troopObj;
    public Troop troop;
    private Color startColor;

    public bool isValid = true;

    private void Start()
    {
        startColor = sr.color;
    }
    private void OnMouseEnter()
    {
        if (UIManager.main.IsHoveringUI()) return;

        if (Time.timeScale == 0f) return;

        if (!isValid) return;

        sr.color = hoverColor;

    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (UIManager.main.IsHoveringUI()) return;

        if (Time.timeScale == 0f) return;

        if (!isValid) return;

        if (troopObj != null) 
        {
            troop.OpenUpgradeUI();
            return; 
        }

        TroopTower tempTroop = TroopManager.main.GetSelectedTroop();

        if (tempTroop.cost > LevelManager.main.money)
        { 
            Debug.Log("You cant afford this");
            return;
        }

        LevelManager.main.SpendMoney(tempTroop.cost);

        troopObj = Instantiate(tempTroop.prefab, transform.position, Quaternion.identity);
        troop = troopObj.GetComponent<Troop>();
        }
}
