/// <summary>
/// AuthenticateUI - Authentication Interface
/// 
/// Main authentication UI handling login/signup flows and credential verification.
/// Manages user account creation and login validation.
/// 
/// Features:
/// - Authentication button interface
/// - Login/signup workflow
/// - Credential validation
/// - UI feedback for authentication status
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthenticateUI : MonoBehaviour
{


    [SerializeField] private Button authenticateButton;


    private void Awake()
    {
        authenticateButton.onClick.AddListener(() =>
        {
            LobbyManager.Instance.Authenticate(EditPlayerName.Instance.GetPlayerName());
            Hide();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        if (LobbyCameraTransitionManager.instance != null)
        {
            LobbyCameraTransitionManager.instance.EnableTransitions();
        }
    }

}