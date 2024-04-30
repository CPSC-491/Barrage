using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Money : MonoBehaviour
{
    public TextMeshPro moneyText;
    private int balance = 0;

    // Function to update money balance and UI
    public void AddMoney(int amount)
    {
        balance += amount;
    }

    // Function to retrieve balance
    public int getBalance()
    {
        return balance;
    }

    // Function to set balance
    public void setBalance(int amount)
    {
        balance = amount;
    }
}
