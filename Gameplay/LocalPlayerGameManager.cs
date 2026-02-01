/// <summary>
/// LocalPlayerGameManager - Local Player Gameplay State
/// 
/// Manages game state and mechanics for the local player instance.
/// Coordinates with game modes and handles local gameplay events.
/// 
/// Features:
/// - Local game state management
/// - Gameplay event coordination
/// - Local player reference handling
/// - Scene management integration
/// 
/// Dependencies: Netcode, Scene Management
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalPlayerGameManager : MonoBehaviour

{
    public static LocalPlayerGameManager Instance;

    public event EventHandler OnLocalPlayerPaused;
    public event EventHandler OnLocalPlayerResumed;
    public event EventHandler OnLocalPlayerQuit;
    public event EventHandler OnLocalPlayerOpenChat;
    public event EventHandler OnLocalPlayerCloseChat;

    public int Ammo;

    public enum LocalState
    {
        Paused,
        Resumed,
        Chat
    }
    public LocalState State;

    public void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        State = LocalState.Resumed;

        string playerName = PlayerPrefs.GetString("PlayerName");
        if (HabibVerse.Instance != null)
        {
            HabibVerse.Instance.SetName(playerName);
        }
    }
    public void Update()
    {
        PauseGame();
    }


    public void PauseGame()
    {

        if (Input.GetKeyDown(KeyCode.P) && State != LocalState.Paused)
        {
            OnLocalPlayerPaused?.Invoke(this, EventArgs.Empty);
            State = LocalState.Paused;
        }
        if (Input.GetKeyDown(KeyCode.C) && State != LocalState.Chat)
        {
            Cursor.visible = true;
            OnLocalPlayerOpenChat?.Invoke(this, EventArgs.Empty);
            State = LocalState.Chat;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && State == LocalState.Chat)
        {
            OnLocalPlayerCloseChat?.Invoke(this, EventArgs.Empty);
            if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("CharacterSelectionScene"))
            {
                ResumeGame();
            }
        }
    }

    public void ResumeGame()
    {
        Cursor.visible = false;
        OnLocalPlayerResumed?.Invoke(this, EventArgs.Empty);
        State = LocalState.Resumed;
    }

    public void QuitGame()
    {
        OnLocalPlayerQuit?.Invoke(this, EventArgs.Empty);
        // Shutting down network manager.
        Cleanup();

    }
    void Cleanup()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.Shutdown();
            Destroy(NetworkManager.Singleton.gameObject);
            SceneManager.LoadSceneAsync("CodeMonkeyLobbyScene");
        }
    }
}
