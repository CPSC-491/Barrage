using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    // Canvas UI
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    public Color defaultColor = Color.white;

    // Panels
    public GameObject popUpPanel;
    public TextMeshProUGUI popUpText;
    public TextMeshProUGUI moneyText;
    public GameObject startPanel;
    public GameObject endPanel;
    public TextMeshProUGUI resultsText;

    // Classes and Index
    private Money userMoney;
    private QuizApi quizData;
    private int currentQuestionIndex;

    // Start is called before the first frame update
    void Start()
    {
        // Deactivate pop-up and end panels
        popUpPanel.SetActive(false);
        endPanel.SetActive(false);

        // Activate start panel
        startPanel.SetActive(true);
        StartCoroutine(WaitForStartPanel());

        // Initialize money class
        userMoney = new Money();
        userMoney.setBalance(0);
        currentQuestionIndex = 0;

        StartCoroutine(FetchQuizData());
    }

    // Function that fetches the questions from the Quiz API
    IEnumerator FetchQuizData()
    {
        string apiURL = "https://opentdb.com/api.php?amount=10&category=17&difficulty=easy&type=multiple";

        // Retrieve questions from API
        using (var webRequest = new WWW(apiURL))
        {
            yield return webRequest;

            // Check if API request is successful
            if(!string.IsNullOrEmpty(webRequest.error))
            {
                Debug.LogError("Failed to fetch quiz data: " + webRequest.error);
            }
            else
            {
                // Deserialize the JSON data
                quizData = JsonUtility.FromJson<QuizApi>(webRequest.text);

                // Check if data is null
                if (quizData != null && quizData.results != null)
                {
                    Debug.Log("Quiz data fetched successfully");
                    Debug.Log("Quiz: " + quizData.results.Count);

                    DisplayQuestion();
                }
                else
                {
                    Debug.LogWarning("Quiz data or results is null or empty");
                }
            }
        }
    }

    // Function displays the question and answer choices to the scene
    void DisplayQuestion()
    {
        if (currentQuestionIndex < quizData.results.Count)
        {
            // Reset button colors
            ResetButtonColor();

            // Display question
            QuestionData currentQuestion = quizData.results[currentQuestionIndex];
            Debug.Log("Question: " + currentQuestion.question);
            questionText.text = currentQuestion.question;

            // Put all answers in a list
            List<string> allAnswers = new List<string>();
            allAnswers.Add(currentQuestion.correct_answer);
            allAnswers.AddRange(currentQuestion.incorrect_answers);

            // Shuffle answers in the list
            ShuffleList(allAnswers);
            Debug.Log(string.Join(", ", allAnswers));

            // Add answer choices to the buttons
            Debug.Log("Starting answerButtons loop. Length: " + answerButtons.Length);
            for (int i = 0; i < answerButtons.Length; i++)
            {
                // Get the TextMeshProUGUI component of the current button
                TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();

                // Assign the answer text to the button text
                buttonText.text = allAnswers[i];

                // Add a click listener to the button
                int index = i; // Capture current index in a local variable
                answerButtons[i].onClick.RemoveAllListeners(); // Clear existing listeners
                answerButtons[i].onClick.AddListener(() => OnAnswerSelected(allAnswers[index], currentQuestion)); // Add new listener
            }

            // Increment Index
            currentQuestionIndex += 1;

        }
        else
        {
            // Display results
            DisplayResult(userMoney.getBalance().ToString());
            endPanel.SetActive(true);
            //StartCoroutine(WaitForEndPanel());
        }
    }

    // Function shuffles the answers choices
    void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    // Function that changes the color of the button clicked based on correctness
    void OnAnswerSelected(string selectedAnswer, QuestionData currentQuestion)
    {
        bool isCorrect = (selectedAnswer == currentQuestion.correct_answer);

        // Find the button corresponding to the selected answer
        foreach (Button button in answerButtons)
        {
            if (button.GetComponentInChildren<TextMeshProUGUI>().text == selectedAnswer)
            {
                Image buttonImage = button.GetComponent<Image>();

                // Change button color based on correctness
                if (isCorrect == true)
                {
                    buttonImage.color = correctColor;

                    // Add 100 coins to user balance
                    userMoney.AddMoney(100);
                    UpdateMoneyText();
                }
                else
                    buttonImage.color = incorrectColor;
            }
        }

        // Display pop-up message based on correctness of question
        popUpText.text = isCorrect ? "Correct!" : "Incorrect!";
        popUpText.color = isCorrect ? correctColor : incorrectColor;
        popUpPanel.SetActive(true);

        StartCoroutine(WaitForPopUp(DisplayQuestion));
    }

    // Function to reset button colors
    void ResetButtonColor()
    {
        // Go through each button and reset button color
        foreach (Button button in answerButtons)
        {
            Image buttonImage = button.GetComponent<Image>();
            buttonImage.color = defaultColor;
        }
    }

    // Updates money UI
    void UpdateMoneyText()
    {
        moneyText.text = userMoney.getBalance().ToString();
    }

    // Function to display a message on the panel
    void DisplayResult(string result)
    {
        // Activate the popUpPanel and set the message text
        resultsText.text = "You earned " + result + " coins!";
    }

    // Function that waits for user to click to continue with quiz
    IEnumerator WaitForPopUp(System.Action callBack)
    {
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }

        // Hide panel
        popUpPanel.SetActive(false);

        // Call Function
        callBack?.Invoke();
    }

    // Function that waits for the user click on the start panel
    IEnumerator WaitForStartPanel()
    {
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }

        // Hide panel
        startPanel.SetActive(false);
    }

    IEnumerator WaitForEndPanel()
    {
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }

        // Hide panel
        endPanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
