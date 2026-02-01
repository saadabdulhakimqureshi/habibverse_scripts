using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import the TextMeshPro namespace
using System.Text.RegularExpressions;

public class Sign_IN : MonoBehaviour
{
    public TMP_InputField passwordInputFieldLogin; // TMP Input Field
    public TMP_InputField emailInputFieldLogin; // TMP Input Field
    public TextMeshProUGUI errorMessageLogin; // TMP Text
    public GameObject dashboard;
    public GameObject signin;

    public void OnLoginButtonClickmain()
    {
        string errorMessagesLogin = "\n"; // Initialize an empty string to collect error messages

        if (!IsValidPasswordLogin(passwordInputFieldLogin.text))
        {
            Debug.Log("password not valid");
            errorMessagesLogin += "Password should be at least 8 characters!\n";
        }

        if (!IsValidEmailLogin(emailInputFieldLogin.text))
        {
            Debug.Log("Email not valid");
            errorMessagesLogin += "Invalid email format!\n";
        }

        if (!string.IsNullOrEmpty(errorMessagesLogin)) // If there are errors
        {
            errorMessageLogin.text = errorMessagesLogin; // Display all the concatenated error messages
            dashboard.SetActive(false);
            signin.SetActive(true);
            return;
        }

        // Continue with the login process...
    }

    public bool IsValidPasswordLogin(string password)
    {
        return password.Length >= 8; // Must be at least 8 characters
    }

    public bool IsValidEmailLogin(string email)
    {
        var regex = new Regex(@"^[a-zA-Z]{2}\d{5}@st\.habib\.edu\.pk$");
        return regex.IsMatch(email);
    }
}
