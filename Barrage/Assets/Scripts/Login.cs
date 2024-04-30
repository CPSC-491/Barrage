using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public Button goToRegisterButton;

    // Start is called before the first frame update
    void Start()
    {
        goToRegisterButton.onClick.AddListener(moveToRegister);
    }

    void moveToRegister()
    {
        SceneManager.LoadScene("Register");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
