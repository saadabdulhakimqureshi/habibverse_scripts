using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class PlayerHUD : NetworkBehaviour
{
    private NetworkVariable<FixedString128Bytes> playerName = new NetworkVariable<FixedString128Bytes>();

    private bool overlaySet = false;

    [Header("HUD References")]
    public TextMeshProUGUI PlayerName;

    public void Start()
    {

    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsServer)
        {
            if (AuthenticationService.Instance != null)
            {
                if (LobbyManager.Instance != null)
                playerName.Value = $"{LobbyManager.Instance.GetPlayerName()}";
                
            }
        }



    }


    public void SetOverlay()
    {
        PlayerName.text = playerName.Value.ToString();
    }



    private void Update()
    {
        if (!overlaySet && !string.IsNullOrEmpty(playerName.Value.ToString()))
        {
            SetOverlay();
            overlaySet = true;
        }
    }
}
