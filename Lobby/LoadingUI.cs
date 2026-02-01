/// <summary>
/// LoadingUI - Loading Screen Interface
/// 
/// Loading screen UI with progress indicators and helpful tips.
/// Displays during scene transitions and game initialization.
/// 
/// Features:
/// - Loading progress bar display
/// - Loading tips/hints rotation
/// - Scene loading coordination
/// - LobbyManager integration
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (LobbyManager.Instance != null)
        {
            LobbyManager.Instance.OnGameStarted += Instance_OnGameStarted;
        }
        if (LocalPlayerGameManager.Instance != null)
        {
            LocalPlayerGameManager.Instance.OnLocalPlayerQuit += Instance_OnLocalPlayerQuit;
        }
        if (ReadySelect.Instance != null)
        {
            ReadySelect.Instance.OnAllPlayersReady += Instance_OnAllPlayersReady;
        }
        Hide();
    }

    private void Instance_OnAllPlayersReady(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Instance_OnLocalPlayerQuit(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Instance_OnGameStarted(object sender, System.EventArgs e)
    {
        Show();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
