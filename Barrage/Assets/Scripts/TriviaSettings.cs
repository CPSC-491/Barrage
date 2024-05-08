using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriviaSettings : MonoBehaviour
{
    [SerializeField] private TMP_Text subjectText;
    public void SubjectDropDown(int index)
    {
        switch (index)
        {
            case 0: subjectText.text = "1"; break;
        }
    }
}
