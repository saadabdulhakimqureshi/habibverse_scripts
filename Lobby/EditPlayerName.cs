/// <summary>
/// EditPlayerName - Player Name Configuration UI
/// 
/// UI for editing and saving the player's display name.
/// Allows players to customize their in-game name.
/// 
/// Features:
/// - Text input field for player name
/// - Name validation
/// - Save/apply name changes
/// - UI text component integration
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditPlayerName : MonoBehaviour
{


    public static EditPlayerName Instance { get; private set; }


    public event EventHandler OnNameChanged;


    [SerializeField] private TextMeshProUGUI playerNameText;


    private string playerName = "PlayerName";


    private void Awake()
    {
        Instance = this;

        GetComponent<Button>().onClick.AddListener(() =>
        {
            UI_InputWindow.Show_Static("Player Name", playerName, "abcdefghijklmnopqrstuvxywzABCDEFGHIJKLMNOPQRSTUVXYWZ .,-", 20,
            () =>
            {
                // Cancel
            },
            (string newName) =>
            {
                playerName = newName;

                playerNameText.text = playerName;

                OnNameChanged?.Invoke(this, EventArgs.Empty);
            });
        });

        playerNameText.text = playerName;
    }

    private void Start()
    {
        OnNameChanged += EditPlayerName_OnNameChanged;
    }

    private void EditPlayerName_OnNameChanged(object sender, EventArgs e)
    {
        LobbyManager.Instance.UpdatePlayerName(GetPlayerName());

        // Saving playername to prefs to access it later.
        PlayerPrefs.SetString("PlayerName", GetPlayerName());
    }

    public string GetPlayerName()
    {
        return playerName;
    }


}