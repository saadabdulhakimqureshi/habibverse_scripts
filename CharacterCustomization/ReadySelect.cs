/// <summary>
/// ReadySelect - Ready State Synchronization
/// 
/// Manages and synchronizes player ready states across the network.
/// Handles player ready/not-ready logic and initiates scene transitions when all players are ready.
/// 
/// Features:
/// - NetworkBehaviour for Netcode synchronization
/// - Ready state tracking for each player
/// - Automatic scene transition when all players ready
/// - Server-side state management
/// 
/// Dependencies: Netcode, UnityEngine.SceneManagement
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadySelect : NetworkBehaviour
{
    // Instance
    public static ReadySelect Instance;

    // Dictionaries
    // Stores ready state for all client ids.
    Dictionary<ulong, bool> playerReadyDictionary;

    // Events
    public event EventHandler OnReadyChange;
    public event EventHandler OnAllPlayersReady;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        playerReadyDictionary = new Dictionary<ulong, bool>();
    }

    // Update is called once per frame
    void Update()
    {
        HabibVerse.Instance.ViewPlayerData();
    }
    public void SetPlayerReady()
    {
        SetPlayerReadyServerRpc();
    }

    // Server updates dictionary for all client ids.
    [ServerRpc(RequireOwnership = false)]
    private void SetPlayerReadyServerRpc(ServerRpcParams serverRpcParams = default)
    {
        // Player is ready.
        SetPlayerReadyClientRpc(serverRpcParams.Receive.SenderClientId);

        playerReadyDictionary[serverRpcParams.Receive.SenderClientId] = true;


        // Checking if all players are ready.
        bool allClientsReady = true;
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            if (!playerReadyDictionary.ContainsKey(clientId) || !playerReadyDictionary[clientId])
            {
                allClientsReady = false;
                break;
            }
        }

        // All players are ready.
        if (allClientsReady)
        {
            OnAllPlayersReady?.Invoke(this, EventArgs.Empty);

            string gameMode = LobbyManager.Instance.GetGameMode();

            // Loading character selection scene.
            if (gameMode == "FreeRoam")
                NetworkManager.Singleton.SceneManager.LoadScene("FreeroamScene", LoadSceneMode.Single);
            else
            {
                NetworkManager.Singleton.SceneManager.LoadScene("TreasureHuntScene", LoadSceneMode.Single);
            }
        }
    }


    // Setting player ready for current client.
    [ClientRpc]
    private void SetPlayerReadyClientRpc(ulong clientId)
    {
        playerReadyDictionary[clientId] = true;

        // Disabling customization when player is ready.
        // Setting ready indication to true.
        OnReadyChange?.Invoke(this, EventArgs.Empty);
    }


    public bool IsPlayerReady(ulong clientId)
    {
        return playerReadyDictionary.ContainsKey(clientId) && playerReadyDictionary[clientId];
    }
}
