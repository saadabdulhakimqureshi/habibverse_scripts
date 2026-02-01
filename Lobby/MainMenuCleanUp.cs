using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MainMenuCleanUp : MonoBehaviour
{


    private void Awake()
    {
        // Destroying Network Manger and Freeroam Manager if they are carrying over from gameplay.
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.Shutdown();
            Destroy(NetworkManager.Singleton.gameObject);
        }

        if (HabibVerse.Instance != null)
        {
            Destroy(HabibVerse.Instance.gameObject);
        }

        if (LobbyManager.Instance != null)
        {
            Destroy(LobbyManager.Instance.gameObject);
        }


    }

}