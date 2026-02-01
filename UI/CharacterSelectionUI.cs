/// <summary>
/// CharacterSelectionUI - Character Selection Interface
/// 
/// Character selection interface allowing players to choose their avatar.
/// Provides UI controls for character selection and customization entry.
/// 
/// Features:
/// - Character preview display
/// - Character selection buttons
/// - Confirm selection interface
/// - Customization flow entry
/// - Cinemachine camera control
/// - Network integration
/// 
/// Dependencies: Cinemachine, Netcode, Input System, TextMeshPro
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.Netcode;
using System;
using TMPro;

public class CharacterSelectionUI : MonoBehaviour
{
    // Start is called before the first frame update




    [Header("Scene References")]
    public CameraManager CameraManager;
    public PlayerCustomizer PlayerCustomizer;

    [Header("Screen References")]
    public GameObject CharacterSelectionScreen;

    // Buttons
    public Button ReadyButton;
    public Button ChatButton;

    // Customization Panels
    public GameObject ModelSelection;
    public GameObject ButtonSelection;
    public GameObject AuraSelection;
    public GameObject FaceSelection;
    public GameObject HatSwitcher;
    public GameObject ModelSwitcher;
    public GameObject ModelColorSelection;
    public GameObject ParticleColorSelection;

    // Chat 
    public GameObject Chat;

    // Color
    public List<Image> colors;

    public void Awake()
    {

    }

    void Start()
    {
        ReadyButton.onClick.AddListener(() =>
        {
            ReadySelect.Instance.SetPlayerReady();
            CameraManager.instance.ShowLobby();
            Hide();
        });

        ChatButton.onClick.AddListener(() =>
        {
            ShowChat();
        });

        ResetCustomization();

        // Getting saved player name and updating player customization data.


    }

    // Update is called once per frame
    void Update()
    {

    }



    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetModelIndex(int index)
    {

        HabibVerse.Instance.SetModelIndex(index);
    }

    public void ShowChat()
    {
        Chat.SetActive(true);
    }

    public void SetModelColor(int index)
    {

        HabibVerse.Instance.SetModelColor(colors[index].color);
    }
    public void SetParticlesColor(int index)
    {
        HabibVerse.Instance.SetParticlesColor(colors[index].color);
    }



    public void ChangeCharacterColor()
    {
        HatSwitcher.SetActive(false);
        FaceSelection.SetActive(false);
        AuraSelection.SetActive(false);
        ButtonSelection.SetActive(false);
        ModelColorSelection.SetActive(true);
        ParticleColorSelection.SetActive(false);
        ModelSelection.SetActive(false);
    }
    public void ChangeParticleColor()
    {
        HatSwitcher.SetActive(false);
        FaceSelection.SetActive(false);
        AuraSelection.SetActive(false);
        ButtonSelection.SetActive(false);
        ModelColorSelection.SetActive(false);
        ParticleColorSelection.SetActive(true);
        ModelSelection.SetActive(false);
    }


    public void DisableCharacterSelection()
    {
        ResetCustomization();
        //CharacterSelection.DisableCharacters();
    }
    public void ChangeFace(int index)
    {

        HabibVerse.Instance.SetFaceIndex(index);

    }

    public void ChangeParticles(int index)
    {
        HabibVerse.Instance.SetAuraIndex(index);
    }

    public void ChangeHat(int index)
    {

        HabibVerse.Instance.SetHatIndex(index);

    }

    public void ToggleModelSelection()
    {
        ButtonSelection.SetActive(false);
        FaceSelection.SetActive(false);
        AuraSelection.SetActive(false);
        HatSwitcher.SetActive(false);
        ModelColorSelection.SetActive(false);
        ParticleColorSelection.SetActive(false);
        ModelSelection.SetActive(true);
    }

    public void ToggleAuraSelection()
    {
        ButtonSelection.SetActive(false);
        FaceSelection.SetActive(false);
        AuraSelection.SetActive(true);
        HatSwitcher.SetActive(false);
        ModelColorSelection.SetActive(false);
        ParticleColorSelection.SetActive(false);
        ModelSelection.SetActive(false);
    }

    public void ToggleFaceSelection()
    {
        ButtonSelection.SetActive(false);
        FaceSelection.SetActive(true);
        AuraSelection.SetActive(false);
        HatSwitcher.SetActive(false);
        ModelColorSelection.SetActive(false);
        ParticleColorSelection.SetActive(false);
        ModelSelection.SetActive(false);
    }

    public void ToggleHatSwitcher()

    {
        ButtonSelection.SetActive(false);
        FaceSelection.SetActive(false);
        AuraSelection.SetActive(false);
        ModelColorSelection.SetActive(false);
        ParticleColorSelection.SetActive(false);
        HatSwitcher.SetActive(true);
        ModelSelection.SetActive(false);
    }

    public void ResetCustomization()
    {
        ButtonSelection.SetActive(true);
        HatSwitcher.SetActive(false);
        FaceSelection.SetActive(false);
        AuraSelection.SetActive(false);
        ModelColorSelection.SetActive(false);
        ParticleColorSelection.SetActive(false);
        ModelSelection.SetActive(false);
    }

    public void BackToButtonSelection()
    {
        HatSwitcher.SetActive(false);
        FaceSelection.SetActive(false);
        AuraSelection.SetActive(false);
        ModelColorSelection.SetActive(false);
        ParticleColorSelection.SetActive(false);
        ButtonSelection.SetActive(true);
        ModelSelection.SetActive(false);
    }


}
