using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;
//using Firebase.Database;

// var user = FirebaseAuth.DefaultInstance.CurrentUser;
// use in other scenes to get user info from firebase

public class LoginManager : MonoBehaviour
{
    // Firebase variables
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    //public DatabaseReference DBreference;

    // Login Page
    public GameObject loginPanel;
    public TMP_InputField emailLoginInput;
    public TMP_InputField passwordLoginInput;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;
    public Button loginButton;
    public Button goToRegisterButton;
    public Button playAsGuestButton;

    // Register Page
    public GameObject registerPanel;
    public TMP_InputField emailRegisterInput;
    public TMP_InputField usernameRegisterInput;
    public TMP_InputField passwordRegisterInput;
    public TMP_InputField confirmPasswordInput;
    public TMP_Dropdown gradeLevelDropdown;
    public TMP_Text warningRegisterText;
    public TMP_Text confirmRegisterText;
    public Button registerButton, backToLoginButton;

    /*
    // User Data Page
    public GameObject userDataPanel;
    public TMP_InputField usernameText;
    public TMP_InputField gradeLevelText;
    public TMP_InputField moneyText;
    public Button continueButton, signOutButton, saveButton;
    */

    void Awake()
    {
        // Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // If they are available, initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    // Initializes Firebase Auth
    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        // Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        //DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Function for login button
    public void LoginButton()
    {
        // Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginInput.text, passwordLoginInput.text));
    }

    // Function for the register button
    public void RegisterButton()
    {
        // Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterInput.text, passwordRegisterInput.text, usernameRegisterInput.text));
    }

    // Skips login and goes straight to the game
    public void PlayAsGuestButton()
    {
        StartCoroutine(Guest());
    }

    /*
    // Function for save button
    public void SaveDataButton()
    {
        StartCoroutine(UpdateUsernameAuth(usernameText.text));
        StartCoroutine(UpdateUsernameDatabase(usernameText.text));

        //StartCoroutine(UpdateGradeLevel(int.Parse(gradeLevelText.text)));
        //StartCoroutine(UpdateMoney(int.Parse(moneyText.text)));
    }

    
    // Function for the sign out button
    public void SignOutButton()
    {

    }

    // Function for the continue button
    public void ContinueButton()
    {
        
    }
    */

    // Login function
    private IEnumerator Login(string _email, string _password)
    {
        // Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);

        // Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            // Handle errors
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Enail";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;

            }
            warningLoginText.text = message;
        }
        else
        {
            // User is now logged in
            // Now get the result
            User = LoginTask.Result.User;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Logged In";
            //Invoke("GoToMainMenu", 3);
            yield return new WaitForSeconds(3);

            // usernameText.text = User.DisplayName;
            // gradeLevelText.text = ;
            // Show user data screen
            //GoToUserData();
            ClearRegisterFields();
            //ClearLoginFields();
        }
    }

    // Function for registering account
    public IEnumerator Register(string _email, string _password, string _username)
    {
        if (_email == "")
        {
            warningRegisterText.text = "Missing email";
        }
        else if (_username == "")
        {
            // if username field is blank, show a warning
            warningRegisterText.text = "Missing Usernname";
        }
        else if(passwordRegisterInput.text != confirmPasswordInput.text)
        {
            // If passwrods do not match
            warningRegisterText.text = "Passwords do not match!";
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            Task<AuthResult> RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);

            // Wait until task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if(RegisterTask.Exception != null)
            {
                // Handle errors
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Alreay In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                // User has been registered
                User = RegisterTask.Result.User;

                if (User != null)
                {
                    // Create a user profile and set the userame
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    // Call the Firebase auth update user profile function passing the profile with the user name
                    Task ProfileTask = User.UpdateUserProfileAsync(profile);

                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        // Handle errors
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed";
                    }

                    // Username is set
                    warningRegisterText.text = "";
                    // Retrieve Grade Level Selection
                    int selectedIndex = gradeLevelDropdown.value;
                    string selectedOption = gradeLevelDropdown.options[selectedIndex].text;
                    //UpdateGradeLevel(int.Parse(selectedOption));

                    // Add grade level to user in the database

                    // Return to Login screen
                    confirmRegisterText.text = "Sign Up Successful!";
                    yield return new WaitForSeconds(3);
                    GoToLogin();
                    ClearRegisterFields();
                    ClearLoginFields();
                }
            }
        }
    }

    // Function for the guest
    public IEnumerator Guest()
    {
        confirmLoginText.text = "Playing as Guest!";
        yield return new WaitForSeconds(3);
        GoToMainMenu();
        ClearLoginFields();
        ClearRegisterFields();
    }

    /*
    // Updates username
    private IEnumerator UpdateUsernameAuth(string _username)
    {
        // Create a user profile and set the username
        UserProfile profile = new UserProfile { DisplayName = _username };

        // Call the Firebase auth update user profile function passing the profile with the name
        var ProfileTask = User.UpdateUserProfileAsync(profile);
        // wait until the task completes
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if (ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        }
        else
        {
            // Auth username is now updated
        }
    }

    // Updates database with the new username change
    private IEnumerator UpdateUsernameDatabase(string _username)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("username").SetValueAsync(_username);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if(DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            // Database username is now updated
        }
    }

    // Updates the user's grade level
    private IEnumerator UpdateGradeLevel(int _gradeLevel)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Grade Level").SetValueAsync(_gradeLevel);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            // Grade level is now updated in the database
        }
    }

    // Updates the user's grade level
    private IEnumerator UpdateMoney(int _money)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Money").SetValueAsync(_money);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            // Money is now updated in the database
        }
    }
    */

    // UI Functions
    public void GoToRegister()
    {
        registerPanel.SetActive(true);
        loginPanel.SetActive(false);
        emailLoginInput.text = "";
        passwordLoginInput.text = "";
        confirmLoginText.text = "";
        warningRegisterText.text = "";
    }

    public void GoToLogin()
    {
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
        emailRegisterInput.text = "";
        usernameRegisterInput.text = "";
        passwordRegisterInput.text = "";
        confirmPasswordInput.text = "";
    }
    /*
    public void GoToUserData()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
        userDataPanel.SetActive(true);
    }
    */
    public void ClearLoginFields()
    {
        emailLoginInput.text = "";
        passwordLoginInput.text = "";
        confirmLoginText.text = "";
        warningRegisterText.text = "";
    }

    public void ClearRegisterFields()
    {
        emailRegisterInput.text = "";
        usernameRegisterInput.text = "";
        passwordRegisterInput.text = "";
        confirmPasswordInput.text = "";
        warningRegisterText.text = "";
        confirmRegisterText.text = "";
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
