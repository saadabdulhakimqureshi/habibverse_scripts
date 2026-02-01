using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisconnectionMessage : MonoBehaviour
{
    public Button Return;
    // Start is called before the first frame update
    void Start()
    {
        Return.onClick.AddListener(() =>
        {
            ReturnToLobbyScene();
        });
        NetworkManager.Singleton.OnClientDisconnectCallback += Singleton_OnClientDisconnectCallback;
        Hide();
    }

    public void Singleton_OnClientDisconnectCallback(ulong clientId)
    {
        if (clientId == NetworkManager.ServerClientId)
        {
            Show();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReturnToLobbyScene()
    {
        if (LocalPlayerGameManager.Instance != null)
        {
            LocalPlayerGameManager.Instance.QuitGame();
        }
    }

    private void Show()
    {
        Cursor.visible = true;
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback -= Singleton_OnClientDisconnectCallback;

    }
}
