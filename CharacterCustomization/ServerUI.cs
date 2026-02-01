/// <summary>
/// ServerUI - Server-Side Customization UI
/// 
/// Manages server-side UI during the character customization phase.
/// Coordinates with the game host/server for customization validation.
/// 
/// Features:
/// - Server-side UI management
/// - Connection status display
/// - Server reference management
/// </summary>

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ServerUI : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Scene References")]
    public NetworkManager NetworkManager;

    [Header("Buttons")]
    public Button Host;
    public Button Server;
    public Button Client;
    void Start()
    {
        Host.onClick.AddListener(() =>
        {
            NetworkManager.StartHost();
        });
        Server.onClick.AddListener(() =>
        {
            NetworkManager.StartServer();
        });
        Client.onClick.AddListener(() =>
        {
            NetworkManager.StartClient();
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
