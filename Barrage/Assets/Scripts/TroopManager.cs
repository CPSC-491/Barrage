using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopManager : MonoBehaviour
{
    public static TroopManager main;

    [Header("References")]
    [SerializeField] private TroopTower[] troopTowers;

    private int selectedTroop = 0;

    private void Awake()
    {
        main = this;
    }

    public TroopTower GetSelectedTroop()
    {
        return troopTowers[selectedTroop];
    }

    public void setSelectedTroop(int _selectedTroop)
    {
        selectedTroop = _selectedTroop;
    }
}
