using System;
using UnityEngine;

[Serializable]
public class TroopTower
{
    public string name;
    public int cost;
    public GameObject prefab;

    public TroopTower(string _name, int _cost, GameObject _prefab)
    {
        name = _name;
        cost = _cost;
        prefab = _prefab;
    }
}
