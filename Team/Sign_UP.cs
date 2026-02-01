using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import the TextMeshPro namespace
using System.Text.RegularExpressions;

public class Sign_UP : MonoBehaviour
{
    //public TMP_InputField HUIDInputField; // TMP Input Field
    public TMP_InputField passwordInputField; // TMP Input Field
    public TMP_InputField batchInputField; // TMP Input Field
    public TMP_InputField emailInputField; // TMP Input Field
    public TMP_InputField nameInputField;
    public TMP_InputField reasonInputField;
    public TMP_InputField majorInputField;

    public TextMeshProUGUI errorMessage; // TMP Text
    public GameObject signup, authentication;

    public void OnLoginButtonClick()
    {
        string errorMessages = ""; // Initialize an empty string to collect error messages

        // Check if 'name', 'batch', and 'reason' are not empty
        if (string.IsNullOrEmpty(majorInputField.text))
        {
            errorMessages += "Major cannot be empty!\n";
        }

        if (string.IsNullOrEmpty(nameInputField.text))
        {
            errorMessages += "Name cannot be empty!\n";
        }

        if (string.IsNullOrEmpty(batchInputField.text))
        {
            errorMessages += "Batch cannot be empty!\n";
        }

        if (string.IsNullOrEmpty(reasonInputField.text))
        {
            errorMessages += "Reason cannot be empty!\n";
        }

        // Existing checks from your code:

        /*if (!IsValidID(HUIDInputField.text))
        {
            errorMessages += "Invalid ID format!\n"; // Concatenate error message
        }*/

        if (!IsValidPassword(passwordInputField.text))
        {
            errorMessages += "Password should be at least 8 characters!\n";
        }

        if (!IsValidBatch(batchInputField.text))
        {
            errorMessages += "Invalid Batch format!\n";
        }

        if (!IsValidEmail(emailInputField.text))
        {
            errorMessages += "Invalid email format!\n";
        }

        // Display the errors, if any
        if (!string.IsNullOrEmpty(errorMessages))
        {
            errorMessage.text = errorMessages; // Display all the concatenated error messages
            authentication.SetActive(false);
            signup.SetActive(true);
            return;
        }

        // Continue with the login process...
    }


    public bool IsValidID(string id)
    {
        var regex = new Regex(@"^[a-zA-Z]{2}\d{4}$");
        return regex.IsMatch(id);
    }

    public bool IsValidPassword(string password)
    {
        return password.Length >= 8; // Modify as per your criteria
    }

    public bool IsValidBatch(string batch)
    {
        var regex = new Regex(@"^20\d{2}$"); // Starts with "20" followed by two more digits
        return regex.IsMatch(batch);
    }

    public bool IsValidEmail(string email)
    {
        var regex = new Regex(@"^[a-zA-Z]{2}\d{5}@st\.habib\.edu\.pk$");
        return regex.IsMatch(email);
    }

}
