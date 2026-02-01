/// <summary>
/// HabibVerse - Main Game Manager
/// 
/// Central orchestrator for the HabibVerse multiplayer game.
/// Manages networked player data, game state, scene transitions, and player list synchronization.
/// 
/// Key Responsibilities:
/// - Maintains NetworkList of all connected players and their customization data
/// - Broadcasts events when player list or individual player data changes
/// - Singleton pattern for global game access
/// - NetworkBehaviour for Netcode multiplayer synchronization
/// 
/// Events:
/// - OnPlayerListChanged: Fired when players join/leave
/// - OnPlayerDataChanged: Fired when player customization data updates
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
using Random = UnityEngine.Random;


public class HabibVerse : NetworkBehaviour
{
    public static HabibVerse Instance;
    public NetworkList<CustomizationData> players;

    public event EventHandler OnPlayerListChanged;
    public event EventHandler OnPlayerDataChanged;

    public void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        players = new NetworkList<CustomizationData>();

        players.OnListChanged += Players_OnListChanged;
    }

    private void Players_OnListChanged(NetworkListEvent<CustomizationData> changeEvent)
    {
        // Whenever our player list changes we invoke this event.
        OnPlayerListChanged?.Invoke(this, EventArgs.Empty); 
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            // Subscribing to events when client connects.
            NetworkManager.Singleton.OnClientConnectedCallback += ClientConnectedHandler;
            NetworkManager.Singleton.OnClientDisconnectCallback += ClientDisconnectedHandler;
        }
        base.OnNetworkSpawn();
    }

    public override void OnNetworkDespawn()
    {
        if (IsServer)
        {
            // Unsubscribing from events when client connects.
            NetworkManager.Singleton.OnClientConnectedCallback -= ClientConnectedHandler;
            NetworkManager.Singleton.OnClientDisconnectCallback -= ClientDisconnectedHandler;
        }
        base.OnNetworkDespawn();
    }

    // Called when client connects.
    void ClientConnectedHandler(ulong clientId)
    {
        // Adding player to network list.
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        players.Add(new CustomizationData(clientId, new Color(r, g, b), new (r, g, b), 0, 0, 0, 0));
    }

    // Called when client disconnects.
    void ClientDisconnectedHandler(ulong clientId) 
    {
        // Removing player from network list.
        for (int i = 0; i< players.Count; i++)
        {
            if (clientId == players[i].clientId)
            {
                players.Remove(players[i]);
                break;  
            }
        }
    }

    // Checking if current player index is greater than total players.
    public bool IsPlayerIndexConnected(int playerIndex)
    {
        return players.Count > playerIndex;
    }

    public bool IsPlayerInSession(ulong clientId)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].clientId == clientId)
            {
                return true;
            }
        }

        return false;
    }

    public int GetPlayerIndex(ulong clientId)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].clientId == clientId)
            {
                return i;
            }
        }

        return -1;
    }


    // Getting the player data at an index.
    public CustomizationData GetCustomizationData(int index)
    {
        return players[index];
    }

    public CustomizationData GetCustomizationData(ulong clientId)
    {
        Debug.Log("Client " + clientId);
        foreach (CustomizationData customizationData in players)
        {
            if (customizationData.clientId == clientId)
            {
                return customizationData;
            }
        }

        return new CustomizationData(clientId, new Color(1, 1, 1), new(1, 1, 1), 0, 0, 0, 0);
    }

    public bool OwnerOfIndex(ulong clientId, int index)
    {
        if (players[index].clientId == clientId)
        {
            return true;
        }
        return false;
    }
    public int GetModelIndex(int index)
    {
        return players[index].modelIndex;
    }
    public string GetPlayerName(int index)
    {
        return players[index].playerName.ToString();
    }
    public Color GetModelColor(int index)
    {
        return players[index].modelColor;
    }
    public Color GetParticlesColor(int index)
    {
        return players[index].particlesColor;
    }
    public int GetHatIndex(int index)
    {
        return players[index].hatIndex;
    }
    public int GetAuraIndex(int index)
    {
        return players[index].auraIndex;
    }
    public int GetFaceIndex(int index)
    {
        return players[index].faceIndex;
    }
    public void SetModelIndex(int index)
    {
        SetModelIndexServerRpc(index);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetModelIndexServerRpc(int index, ServerRpcParams serverRpcParams = default)
    {
        for (int i = 0; i < players.Count; i++)
        {
           
            if (serverRpcParams.Receive.SenderClientId == players[i].clientId)
            {
                CustomizationData cusomizationData = players[i];
                cusomizationData.modelIndex = index;
                players[i] = cusomizationData;
            }
        }
        OnPlayerDataChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetModelColor(Color color )
    {
        SetModelColorServerRpc(color);
    }

    [ServerRpc(RequireOwnership =false)]
    public void SetModelColorServerRpc(Color Color, ServerRpcParams serverRpcParams = default)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (serverRpcParams.Receive.SenderClientId == players[i].clientId)
            {
                CustomizationData cusomizationData = players[i];
                cusomizationData.modelColor = Color;
                players[i] = cusomizationData;
            }
        }
        OnPlayerDataChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetParticlesColor(Color color)
    {
        SetParticlesColorServerRpc(color);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetParticlesColorServerRpc(Color Color, ServerRpcParams serverRpcParams = default)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (serverRpcParams.Receive.SenderClientId == players[i].clientId)
            {
                CustomizationData cusomizationData = players[i];
                cusomizationData.particlesColor = Color;
                players[i] = cusomizationData;
            }
        }
        OnPlayerDataChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetHatIndex(int index)
    {
        SetHatIndexServerRpc(index);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetHatIndexServerRpc(int index, ServerRpcParams serverRpcParams = default)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (serverRpcParams.Receive.SenderClientId == players[i].clientId)
            {
                CustomizationData cusomizationData = players[i];
                cusomizationData.hatIndex = index;
                players[i] = cusomizationData;
            }
        }
        OnPlayerDataChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetAuraIndex(int index)
    {
        SetAuraIndexServerRpc(index);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetAuraIndexServerRpc(int index, ServerRpcParams serverRpcParams = default)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (serverRpcParams.Receive.SenderClientId == players[i].clientId)
            {
                CustomizationData cusomizationData = players[i];
                cusomizationData.auraIndex = index;
                players[i] = cusomizationData;
            }
        }
        OnPlayerDataChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetFaceIndex(int index)
    {
        SetFaceIndexServerRpc(index);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetFaceIndexServerRpc(int index, ServerRpcParams serverRpcParams = default)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (serverRpcParams.Receive.SenderClientId == players[i].clientId)
            {
                CustomizationData cusomizationData = players[i];
                cusomizationData.faceIndex = index;
                players[i]=cusomizationData;
            }
        }
        OnPlayerDataChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetName(string name)
    {
        SetNameServerRpc(name);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetNameServerRpc(string name, ServerRpcParams serverRpcParams = default)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (serverRpcParams.Receive.SenderClientId == players[i].clientId)
            {
                CustomizationData cusomizationData = players[i];
                cusomizationData.playerName = name;
                players[i] = cusomizationData;
            }
        }
        OnPlayerDataChanged?.Invoke(this, EventArgs.Empty);
    }

    public void ViewPlayerData()
    {
        for (int i = 0; i < players.Count; i++)
        {
            Debug.Log("index " + players[i].clientId);
            Debug.Log("Client id " + players[i].clientId);
            Debug.Log("Color " + players[i].modelColor);
        }

        
    }
}

