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

    [SerializeField] private Image opBackground;
    [SerializeField] private Color rightC;
    [SerializeField] private Color wrongC;

    string[] op = { "+", "-", "X", "/" };

    // Text Componenets.
    TextMeshProUGUI txtA;
    TextMeshProUGUI txtB;
    TextMeshProUGUI txtC;
    TextMeshProUGUI txtOp;

    string tempOp;

    // These button methods are to update the game when they are clicked.
    // If correct the button should light up green and so should the answer
    // If incorrect the button should become red
    public void plus_button() 
    {
        if (tempOp == "+") {
            correctAnswer();
        }
        else
        {
            wrongAnswer();
        }
        txtOp.text = "+";
    }

    public void minus_button() 
    {
        if (tempOp == "-")
        {
            correctAnswer();
        }
        else
        {
            wrongAnswer();
        }
        txtOp.text = "-";
    }

    public void mul_button()
    {
        if (tempOp == "X")
        {
            correctAnswer();
        }
        else
        {
            wrongAnswer();
        }
        txtOp.text = "X";
    }

    public void div_button()
    {
        if (tempOp == "/")
        {
            correctAnswer();
        }
        else
        {
            wrongAnswer();
        }
        txtOp.text = "/";
    }

    public void reset_button() 
    {
        txtOp.text = "?";
        int tempA = Random.Range(1, 10);
        int tempB = Random.Range(1, 10);
        tempOp =  op[Random.Range(0,4)];
        string[] txtArr = findC(tempA, tempB, tempOp);
        txtA.text = txtArr[0];
        txtB.text = txtArr[1];
        txtC.text = txtArr[2];
        
        Debug.Log("reset button");
    }

    private string[] findC(int tA, int tB, string op) {
        string[] tArr = {tA.ToString(), tB.ToString(), "?"};
        if (op == "+") {
            tArr[2] = (tA + tB).ToString();
        }
        else if (op == "-") {
            tArr[2] = (tA - tB).ToString();
        }
        else if (op == "X")
        {
            tArr[2] = (tA * tB).ToString();
        }
        else if (op == "/")
        {
            tArr[0] = (tA * tB).ToString();
            tArr[2] = tA.ToString();
        }
        return tArr;
    }

    //This is to hide a game element
    //public void showMark() { 
    //    if (qMark.activeInHierarchy == false)
    //    {
    //        qMark.SetActive(true);
    //    }
    //}

    //public void hideMark() { 
    //    if (qMark.activeInHierarchy == true) { qMark.SetActive(false); }
    //

    public void correctAnswer() {
        opBackground.color = rightC;
        Debug.Log("Correct Answer");
    }

    public void wrongAnswer() {
        //change it so that it becomes red when wrong answer is clicked
        opBackground.color = wrongC;
        Debug.Log("Sorry, incorrect");
    }

     private void Start()
    {
        // This gets text component to edit text
        txtA = tmpA.GetComponent<TextMeshProUGUI>();
        txtB = tmpB.GetComponent<TextMeshProUGUI>();
        txtC = tmpC.GetComponent<TextMeshProUGUI>();
        txtOp = tmpOp.GetComponent<TextMeshProUGUI>();

        wrongC.a = 1;
        rightC.a = 1;
    }
}
