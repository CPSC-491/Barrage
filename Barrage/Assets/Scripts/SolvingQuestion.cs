using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SolvingQuestion : MonoBehaviour
{
    private int int_solution = findSolution();

    private Text problemText;
    private int a = Random.Range(0, 10);
    private int b = Random.Range(0, 10);
    private string[] opers = { "+", "-", "x", "/" };
    private string q_oper = chooseOperator();

    private void Update()
    {
        problemText.text = "2 x 4 = 8";
    }

    static int findSolution() {
        return Random.Range(2, 20);
    }

    static string chooseOperator()
    {
        int rnd_op = Random.Range(0, 4);
        return opers[rnd_op];
    }
}
