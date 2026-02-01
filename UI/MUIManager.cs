/// <summary>
/// MUIManager - Main UI Manager
/// 
/// Central UI manager coordinating all UI panels and screens.
/// Handles main menu and general UI orchestration.
/// 
/// Features:
/// - UI panel management
/// - Menu navigation
/// - Screen transitions
/// - Input handling
/// - Cinemachine camera integration
/// 
/// Dependencies: Cinemachine, Netcode, Input System
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.Netcode;
using System;
using TMPro;

public class MUIManager : MonoBehaviour
{
    // Start is called before the first frame update



    public TMP_Text TotalPlayersText;
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {



        if (TotalPlayersText != null)
        {
            if (PlayerManager.instance != null)
            {
                TotalPlayersText.text = PlayerManager.instance.PlayersInGame.ToString();
            }
        }
    }


    public void Host()
    {
        if (NetworkManager.Singleton.StartHost())
        {
            Debug.Log("Host started");
        }
        else
        {
            Debug.Log("Host could not be started.");
        }
    }

    public void Server()
    {
        if (NetworkManager.Singleton.StartServer())
        {
            Debug.Log("Server started");
        }
        else
        {
            Debug.Log("Server could not be started.");
        }
    }

    public void Client()
    {
        if (NetworkManager.Singleton.StartClient())
        {
            Debug.Log("Client started");
        }
        else
        {
            Debug.Log("Client could not be started.");
        }
    }
}
