using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour

{
    [SerializeField] Button Resume;
    [SerializeField] Button Quit;

    public void Start()
    {
        Resume.onClick.AddListener(() =>
        {
            LocalPlayerGameManager.Instance.ResumeGame();
            Hide();
        });
        Quit.onClick.AddListener(() =>
        {
            ReturnToLobbyScene();
        });
        if (LocalPlayerGameManager.Instance != null)
        {
            LocalPlayerGameManager.Instance.OnLocalPlayerResumed += Instance_OnLocalPlayerResumed; ;
            LocalPlayerGameManager.Instance.OnLocalPlayerPaused += Instance_OnLocalPlayerPaused;
        }
        Hide();
    }

    private void Instance_OnLocalPlayerResumed(object sender, EventArgs e)
    {
        Cursor.visible = false;
        
        Hide();
    }

    private void Instance_OnLocalPlayerPaused(object sender, EventArgs e)
    {
        Cursor.visible = true;
        Show();
    }

    void Show()
    {
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ReturnToLobbyScene()
    {
        
        if (LocalPlayerGameManager.Instance != null)
            LocalPlayerGameManager.Instance.QuitGame();
        
    }
    

}
