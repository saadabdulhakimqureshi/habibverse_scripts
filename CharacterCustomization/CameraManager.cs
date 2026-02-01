/// <summary>
/// CameraManager - Customization Scene Camera Controller
/// 
/// Manages virtual camera transitions between customization and lobby scenes.
/// Provides smooth camera switching and focus management using Cinemachine.
/// 
/// Features:
/// - Virtual camera management for CharacterSelection scene
/// - Virtual camera management for Lobby scene
/// - Singleton pattern for easy access
/// - Smooth camera transitions
/// 
/// Dependencies: Cinemachine, Netcode
/// </summary>

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    [Header("Scene References")]
    public CinemachineVirtualCamera CharacterSelectionCamera;
    public CinemachineVirtualCamera LobbyCamera;

    public void Awake()
    {
        instance = this;
    }

    public void ChangeCharacter(Transform characterTransform)
    {
        CharacterSelectionCamera.LookAt = characterTransform;
        CharacterSelectionCamera.Follow = characterTransform;
    }

    public void ShowLobby()
    {
        CharacterSelectionCamera.Priority = 0;
        LobbyCamera.Priority = 1;
    }

    public void ShowCharacter()
    {
        CharacterSelectionCamera.Priority = 1;
        LobbyCamera.Priority = 0;
    }
}
