using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopManager : MonoBehaviour
{
    public static TroopManager main;

    [Header("References")]
    [SerializeField] private GameObject[] troopPrefabs;

    private int selectedTroop = 0;

    private void Awake()
    {
        main = this;
    }

    public GameObject GetSelectedTroop()
    {
        return troopPrefabs[selectedTroop];
    }
}
