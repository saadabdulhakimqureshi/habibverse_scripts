/// <summary>
/// PlayerCustomizer - Character Customization Hub
/// 
/// Central manager for the entire character customization flow.
/// Coordinates between the slime model, camera, UI, and networking to create an intuitive character creation experience.
/// 
/// Key Responsibilities:
/// - Manages player index and customization data
/// - Listens to global player list and data change events
/// - Integrates Cinemachine virtual cameras for smooth customization view
/// - Synchronizes customization with network players
/// 
/// Dependencies: HabibVerse (singleton), Cinemachine, Netcode
/// </summary>

using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerCustomizer : MonoBehaviour
{
    [SerializeField] public int playerIndex;

    public int index;

    public GameObject CharacterSelectionCamera;
    // Start is called before the first frame update
    void Start()
    {
        HabibVerse.Instance.OnPlayerListChanged += Instance_OnPlayerListChanged;
        HabibVerse.Instance.OnPlayerDataChanged += Instance_OnPlayerDataChanged;

        // If our player index is equal to clients player index then we make our camera look at model.
/*        if (NetworkManager.Singleton.IsClient)
        {
            if (HabibVerse.Instance.OwnerOfIndex(NetworkManager.Singleton.LocalClientId, playerIndex))
            {
                CameraManager.instance.ChangeCharacter(transform);
            }
        }*/
        
        // Add Subscribtion to Update Model.
        UpdataPlayers();
    }

    private void Instance_OnPlayerListChanged(object sender, System.EventArgs e)
    {
        // Whenver our players leave or connect we either show or hide the models.
        UpdataPlayers();
    }
    private void Instance_OnPlayerDataChanged(object sender, System.EventArgs e)
    {
        // Whenver our players data updates we update their visual.
        UpdataPlayers();
    }

    // Depending on current number of players we either show or hide the current player model in lobby.
    void UpdataPlayers()
    {
        if (HabibVerse.Instance.IsPlayerIndexConnected(playerIndex))
        {
            // Showing or hiding our model. 
            Show();

            // Updating visuals.
            SetModel();
            SetModelColor();
            SetHat();
            SetAura();
            SetFace();
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

    void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetModel()
    {
        int index = HabibVerse.Instance.GetModelIndex(playerIndex);
        GetComponent<SlimeModel>().SetModel(index);
    }
    public void SetModelColor()
    {
        Color color = HabibVerse.Instance.GetModelColor(playerIndex);
        GetComponent<SlimeMaterialColor>().SetColor(color);
    }

    public void SetParticleColor()
    {
        Color color = HabibVerse.Instance.GetParticlesColor(playerIndex);
        GetComponent<SlimeParticleColor>().SetColor(color);
    }

    public void SetHat()
    {
        int index = HabibVerse.Instance.GetHatIndex(playerIndex);
        GetComponent<HatSpawn>().SpawnHat(index);
    }

    public void SetAura()
    {
        int index = HabibVerse.Instance.GetAuraIndex(playerIndex);
        GetComponent<SlimeParticle>().ChangeParticles(index);
    }

    public void SetFace()
    {
        int index = HabibVerse.Instance.GetFaceIndex(playerIndex);
        GetComponent<SlimeFace>().ChangeFace(index);
    }

    public void SetModel(int index)
    {
        GetComponent<SlimeModel>().SetModel(index);
    }
    public void SetModelColor(Color color)
    {
        
        GetComponent<SlimeMaterialColor>().SetColor(color);
    }

    public void SetParticleColor(Color color)
    {
        GetComponent<SlimeParticleColor>().SetColor(color);
    }

    public void SetHat(int index)
    {
        GetComponent<HatSpawn>().SpawnHat(index);
    }

    public void SetAura(int index)
    {
        GetComponent<SlimeParticle>().ChangeParticles(index);
    }

    public void SetFace(int index)
    {
        GetComponent<SlimeFace>().ChangeFace(index);
    }
}
