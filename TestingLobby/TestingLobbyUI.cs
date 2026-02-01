/// <summary>
/// TestingLobbyUI - Testing Lobby Interface
/// 
/// Testing and debugging utility UI for development and testing gameplay scenarios.
/// Provides debugging tools for testing in lobby environment.
/// 
/// Features:
/// - Testing controls
/// - Debug buttons
/// - Scene switching
/// - Development utilities
/// 
/// Dependencies: Netcode, Scene Management
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestingLobbyUI : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Scene References")]
    public NetworkManager NetworkManager;

    [Header("Buttons")]
    public Button CreateGame;
    public Button JoinGame;

    void Start()
    {
        CreateGame.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            NetworkManager.Singleton.SceneManager.LoadScene("CharacterSelectionScene", LoadSceneMode.Single);

        });
        JoinGame.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });

    }

    // Update is called once per frame
    void Update()
    {

    }
}
