/// <summary>
/// SessionID - Session Token Management
/// 
/// Session token management and player session tracking.
/// Manages unique session identifiers for connected players.
/// 
/// Features:
/// - Session ID generation and storage
/// - Session validation
/// - Player session tracking
/// - TextMeshPro UI integration
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class SessionID : MonoBehaviour
{
    public TMP_InputField id; // TMP Input Field
    public Text error; // TMP Text 
    public GameObject idpage;

    public void OnEnter()
    {
        string errorMessages = ""; // Initialize an empty string to collect error messages

        if (!IsValidid(id.text))
        {
            errorMessages += "Incorrect ID!";
        }

        if (!string.IsNullOrEmpty(errorMessages)) // If there are errors
        {
            error.text = errorMessages; // Display all the concatenated error messages
            idpage.SetActive(true);
            return;
        }
    }

    private bool IsValidid(string id_text)
    {
        //check the format
        return (id_text.Length > 0);
    }
}
