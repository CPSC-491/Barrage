using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TriviaData
{
    public int subjectIndex;
    public int difficultyIndex;

    public string subject;
    public string difficulty;

    public TriviaData(TriviaSettings settings)
    {
        subjectIndex = settings.subjectIndex;
        difficultyIndex = settings.difficultyIndex;
        subject = settings.subject;
        difficulty = settings.difficulty;
    }
}
