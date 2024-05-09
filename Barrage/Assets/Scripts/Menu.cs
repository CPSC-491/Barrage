using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI moneyUI;
    [SerializeField] TextMeshProUGUI hpUI;

    private void OnGUI()
    {
        moneyUI.text = "Money: " + LevelManager.main.money.ToString();
        hpUI.text = LevelManager.main.playerHP.ToString();

    }
}
