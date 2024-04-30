using System;
using System.Collections.Generic;

[Serializable]
public class QuizApi
{
    public int response_code;
    public List<QuestionData> results;
}

[Serializable]
public class QuestionData
{
    public string type;
    public string difficulty;
    public string category;
    public string question;
    public string correct_answer;
    public List<string> incorrect_answers;
}

