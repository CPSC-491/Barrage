using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Register : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField confirmPasswordInput;
    public Button registerButton;
    public Button backToLoginButton;

    // Start is called before the first frame update
    void Start()
    {
        backToLoginButton.onClick.AddListener(goToLoginScene);
    }

    void goToLoginScene()
    {
        SceneManager.LoadScene("Login");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
