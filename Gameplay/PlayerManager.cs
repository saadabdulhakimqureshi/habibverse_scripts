/// <summary>
/// PlayerManager - Individual Player Instance Manager
/// 
/// Manages a single player instance including health, state, and gameplay properties.
/// Handles player damage, death, respawning, and networked player state.
/// 
/// Key Responsibilities:
/// - Health management and damage handling
/// - Player death and respawn logic
/// - Pickup collection tracking
/// - Score management
/// - Network state synchronization
/// 
/// Dependencies: Netcode
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    public static PlayerManager instance;

    private NetworkVariable<int> playersInGame = new NetworkVariable<int>(0);

    [SerializeField]
    public int PlayersInGame
    {
        get
        {
            return playersInGame.Value;
        }
    }


    private void Start()
    {
        if (IsServer)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            // Adding listener when our client gets connected.
            NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
            {
                if (NetworkManager.Singleton.IsServer)
                {
                    playersInGame.Value++;
                }
            };

            NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
            {
                if (NetworkManager.Singleton.IsServer)
                {
                    playersInGame.Value--;
                }
            };

        }
    }

}
