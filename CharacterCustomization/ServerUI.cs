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
        Host.onClick.AddListener(() => {
            NetworkManager.StartHost();
        });
        Server.onClick.AddListener(() => {
            NetworkManager.StartServer();
        });
        Client.onClick.AddListener(() => {
            NetworkManager.StartClient();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
