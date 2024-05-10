using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriviaSettings : MonoBehaviour
{
    public TextMeshProUGUI triviaSubject;
    public TextMeshProUGUI triviaDifficulty;

    [SerializeField] private TMP_Dropdown subjectDD;
    [SerializeField] private TMP_Dropdown difficultyDD;

    public int subjectIndex;
    public int difficultyIndex;

    public string subject;
    public string difficulty;

    public void HandleSubjectDropDown()
    {
        subjectIndex = subjectDD.value;
        subject = subjectDD.options[subjectIndex].text;
        triviaSubject.text = "Current Subject: " + subject;
    }

    public void HandleDifficultyDropDown() 
    {
        difficultyIndex = difficultyDD.value;
        difficulty = difficultyDD.options[difficultyIndex].text;
        triviaDifficulty.text = "Trivia Difficulty: " + difficulty;
    }
    public void LoadDiffSubject()
    {
        TriviaData data = SaveTriviaSettings.LoadTSettings();
        subject = data.subject;
        difficulty = data.difficulty;
        subjectIndex = data.subjectIndex;
        difficultyIndex = data.difficultyIndex;

        subjectDD.value = subjectIndex;
        difficultyDD.value = difficultyIndex;
    }

    public void SaveDiffSubject()
    {
        SaveTriviaSettings.SaveTSettings(this);
    }

    public void Start()
    {
        LoadDiffSubject();
        HandleDifficultyDropDown();
        HandleSubjectDropDown();
    }
}
