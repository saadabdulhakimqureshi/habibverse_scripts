using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;


public class LobbyChatBehaviour : NetworkBehaviour {
    [SerializeField] private ChatMessage chatMessagePrefab;
    [SerializeField] private Transform messageParent;
    [SerializeField] private TMP_InputField chatInputField;

    private const int MaxNumberOfMessagesInList = 20;
    public List<ChatMessage> _messages;
    [SerializeField] private List<string> profanityFilter = new List<string> { "profane1", "profane2", "profane3" };

    private const float MinIntervalBetweenChatMessages = 1f;
    private float _clientSendTimer;
        

    private void Start() {
        LoadProfanityFilter();
        _messages = new List<ChatMessage>();
    }

    private void Update() {
        _clientSendTimer += Time.deltaTime;
            
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (chatInputField.text.Length > 0 && _clientSendTimer > MinIntervalBetweenChatMessages) {
                SendMessage(); 
                chatInputField.DeactivateInputField(clearSelection: true);
            } else {
                chatInputField.Select();
                chatInputField.ActivateInputField();
            }
        }
    }

    public void SendMessage() {
        string message = chatInputField.text;
        chatInputField.text = "";
            
        if (string.IsNullOrWhiteSpace(message)) {
            return;
        }

        _clientSendTimer = 0;
        SendChatMessageServerRpc(message, NetworkManager.Singleton.LocalClientId);
    }

    private void AddMessage(string message, ulong senderPlayerId) {
        var msg = Instantiate(chatMessagePrefab, messageParent);
        message = FilterProfanity(message);

        int playerIndex = HabibVerse.Instance.GetPlayerIndex(senderPlayerId);
        msg.SetMessage(HabibVerse.Instance.players[playerIndex].playerName.ToString(), message);
            
        _messages.Insert(0, msg);

        // Set the position based on the index
        Vector3 oldPosition = msg.transform.localPosition;
        Vector3 newPosition = new Vector3(oldPosition.x, oldPosition.y +  (_messages.Count-1) * 40f, oldPosition.z); // Adjust the Y position as needed
        msg.transform.localPosition = newPosition;

        if (_messages.Count > MaxNumberOfMessagesInList) {
            Destroy(_messages[0]);
            _messages.RemoveAt(0);
        }
    }

    [ClientRpc]
    private void ReceiveChatMessageClientRpc(string message, ulong senderPlayerId) {
        AddMessage(message, senderPlayerId);
    }
        
    [ServerRpc(RequireOwnership = false)]
    private void SendChatMessageServerRpc(string message, ulong senderPlayerId) {
        ReceiveChatMessageClientRpc(message, senderPlayerId);
    }


    private void LoadProfanityFilter()
    {
        TextAsset profanityJson = Resources.Load<TextAsset>("word");
        if (profanityJson != null)
        {
            profanityFilter = JsonUtility.FromJson<List<string>>(profanityJson.text);
        }

    }
    private string FilterProfanity(string message)
    {
        foreach (string profaneWord in profanityFilter)
        {
            // Replace profane words with asterisks or your preferred method of filtering
            message = message.Replace(profaneWord, "****");
        }
        return message;
    }
}
