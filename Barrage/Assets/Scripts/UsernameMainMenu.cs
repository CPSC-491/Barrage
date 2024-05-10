using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Threading.Tasks;

public class UsernameMainMenu : MonoBehaviour
{
    public TMP_Text usernameText;
    private FirebaseAuth auth;
    private DatabaseReference DBreference;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize Firebase Auth and Database references
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;

        // Check if a user is authenticated
        FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            // User is signed in, retrieve username from the database
            string userId = user.UserId;
            StartCoroutine(RetrieveUsername(userId));
        }
        else
        {
            // No user is signed in, handle this case (e.g., show login UI)
            Debug.LogWarning("No user is currently signed in.");
        }
    }

    // Function to retrieve the username from the database
    private IEnumerator RetrieveUsername(string userId)
    {
        // Get the currently logged in user data
        Task<DataSnapshot> DBTask = DBreference.Child("users").Child(userId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            usernameText.text = "";
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            usernameText.text = "Welcome: " + snapshot.Child("username").Value.ToString();
            // usernameText.text = snapshot.Child("Username").Value.ToString();
        }
    }

}
