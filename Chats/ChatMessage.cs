/// <summary>
/// ChatMessage - Chat Message Display Component
/// 
/// Individual chat message data structure and display component.
/// Renders chat messages with player name and message text.
/// 
/// Features:
/// - Message text display
/// - Player name attribution
/// - TextMeshPro UI rendering
/// </summary>

using TMPro;
using UnityEngine;


public class ChatMessage : MonoBehaviour
{
    [SerializeField] private TMP_Text textField;

    public void SetMessage(string playerName, string message)
    {
        textField.text = $"<color=grey>{playerName}</color>: {message}";
    }
}