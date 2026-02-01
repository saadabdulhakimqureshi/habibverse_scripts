using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour
{
    public GameObject Ready;

    public TextMeshProUGUI PlayerName;


    public int playerIndex;

    

    // Start is called before the first frame update
    void Start()
    {
        
        HabibVerse.Instance.OnPlayerListChanged += Instance_OnPlayerListChanged;
        ReadySelect.Instance.OnReadyChange += Instance_OnReadyChange;
        
        CheckPlayers();
    }

    private void Instance_OnReadyChange(object sender, EventArgs e)
    {
        // When our players are ready we indicate it.
        CheckPlayers();
    }

    private void Instance_OnPlayerListChanged(object sender, System.EventArgs e)
    {
        // Whenver our players leave or connect we either show or hide the models.
        CheckPlayers();
    }

    // Depending on current number of players we either show or hide the current player model in lobby.
    void CheckPlayers()
    {
        if (HabibVerse.Instance.IsPlayerIndexConnected(playerIndex))
        {
            Show();
            if (tag == "NonPrefab") 
            {
                // Checks if current player is ready.
                CustomizationData playerData = HabibVerse.Instance.GetCustomizationData(playerIndex);
                PlayerName.text = playerData.playerName.ToString();
                //Debug.Log("Character " + playerIndex + " requesting Ready for client id " + playerData.clientId);
                Ready.SetActive(ReadySelect.Instance.IsPlayerReady(playerData.clientId));
            }

        }
        else
        {
            Hide();
        }
    }

    void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void HideReady()
    {
        Ready.SetActive(false);
    }

    public void SetPlayerName(string name)
    {
        PlayerName.text = name;

    }
}
