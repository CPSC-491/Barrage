using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.UI;

public class MC_Buttons : MonoBehaviour
{
    // MC UI Text GameObjects
    public GameObject tmpA;
    public GameObject tmpB;
    public GameObject tmpC;
    public GameObject tmpOp;

    //public Button plus;
    //public Button minus;
    //public Button multiply;
    //public Button divide;

    // MC Button Settings
    //public Color wantedColor;
    //private Color originalColor;
    //private ColorBlock cb;

    // Game Variables (what we want them to display at start)
    public int displayA;
    public int displayB;
    public int displayC;
    public string displayOp;

    string[] op = { "+", "-", "X", "/" };

    // Text Componenets.
    TextMeshProUGUI txtA;
    TextMeshProUGUI txtB;
    TextMeshProUGUI txtC;
    TextMeshProUGUI txtOp;




    // These button methods are to update the game when they are clicked.
    // If correct the button should light up green and so should the answer
    // If incorrect the button should become red
    public void plus_button() 
    {
        if (txtOp.text == "+") {
            Debug.Log("This is the correct answer!");
        }
        else
        {
            Debug.Log("Sorry, incorrect");
        }
        Debug.Log("plus button");
    }

    public void minus_button() 
    {
        if (txtOp.text == "-")
        {
            Debug.Log("This is the correct answer!");
        }
        else
        {
            Debug.Log("Sorry, incorrect");
        }
        Debug.Log("minus button");
    }

    public void mul_button()
    {
        if (txtOp.text == "X")
        {
            Debug.Log("This is the correct answer!");
        }
        else
        {
            Debug.Log("Sorry, incorrect");
        }
        Debug.Log("multiply button");
    }

    public void div_button()
    {
        if (txtOp.text == "/")
        {
            Debug.Log("This is the correct answer!");
        }
        else
        {
            Debug.Log("Sorry, incorrect");
        }
        Debug.Log("divide button");
    }

    public void start_button() 
    {
        int tempA = Random.Range(0, 10);
        int tempB = Random.Range(0, 10);
        txtA.text = tempA.ToString();
        txtB.text = tempB.ToString();
        txtOp.text = op[Random.Range(0,4)];
        txtC.text = findC(tempA, tempB, txtOp.text);
        
        Debug.Log("start button");
    }

    private string findC(int tA, int tB, string op) {
        if (op == "+") {
            return (tA + tB).ToString();
        }
        else if (op == "-") {
            return (tA - tB).ToString();
        }
        else if (op == "X")
        {
            return (tA * tB).ToString();
        }
        else if (op == "/")
        {
            return "?";
        }
        return "-1 error";
    }

     void Start()
    {
        // This gets text component to edit text
        txtA = tmpA.GetComponent<TextMeshProUGUI>();
        txtB = tmpB.GetComponent<TextMeshProUGUI>();
        txtC = tmpC.GetComponent<TextMeshProUGUI>();
        txtOp = tmpOp.GetComponent<TextMeshProUGUI>();
    }
}
