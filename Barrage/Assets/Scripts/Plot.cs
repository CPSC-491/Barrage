using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Sprite emptyTile;

    public GameObject troopObj;
    public Troop troop;
    private Color startColor;

    public bool isValid = true;

    private void Start()
    {
        startColor = sr.color;
        Invoke("CheckIfTileValid", 2.5f);
    }
    private void OnMouseEnter()
    {
        if (UIManager.main.IsHoveringUI() || Time.timeScale == 0f || !isValid) return;

        sr.color = hoverColor;

    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (UIManager.main.IsHoveringUI() || Time.timeScale == 0f || !isValid) return;

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

    public void CheckIfTileValid()
    {
        if (sr.sprite != emptyTile)
        {
            isValid = false;
        }
    }
}
