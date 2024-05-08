using UnityEngine;

[CreateAssetMenu(fileName = "GameInfo", menuName = "Persistence")]
public class GameInfo : ScriptableObject
{
    public bool isNextScene = true;
    public string currentTriviaDifficulty = "17";
    public string currentSubject= "easy";

    private void OnEnable()
    {
        isNextScene = true;
        currentSubject = "easy";
        currentTriviaDifficulty = "17";
    }
}
