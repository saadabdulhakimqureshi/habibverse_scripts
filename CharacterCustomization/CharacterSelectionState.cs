/// <summary>
/// CharacterSelectionState - Character Selection UI State Machine
/// 
/// Manages the character selection interface and state transitions.
/// Handles UI interactions, customization options, and scene progression.
/// 
/// Dependencies: Cinemachine, Netcode, Unity Services
/// </summary>

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using Unity.Services.Authentication;
using UnityEngine.TextCore.Text;
using UnityEditor;
using System;
using Unity.VisualScripting;


public class CharacterSelectionState : MonoBehaviour
{
/*    // Start is called before the first frame update.
    
    public List<GameObject> characters;

    public int index;

    public GameObject CharacterSelectionCamera;
*/
    
/*    void Start()
    {
        
        index = 0;
        CharacterSelectionCamera.GetComponent<CinemachineVirtualCamera>().LookAt = characters[index].transform;
        CharacterSelectionCamera.GetComponent<CinemachineVirtualCamera>().Follow = characters[index].transform;
    }

    

    public void moveLeft()
    {

        index = (index - 1 + characters.Count) % characters.Count;
        CharacterSelectionCamera.GetComponent<CinemachineVirtualCamera>().LookAt = characters[index].transform;
        CharacterSelectionCamera.GetComponent<CinemachineVirtualCamera>().Follow = characters[index].transform;

    }

    public void moveRight()
    {

        index = (index + 1) % characters.Count;
        CharacterSelectionCamera.GetComponent<CinemachineVirtualCamera>().LookAt = characters[index].transform;
        CharacterSelectionCamera.GetComponent<CinemachineVirtualCamera>().Follow = characters[index].transform;
*/
    }

/*    public void ChangeHatRight()
    {

        characters[index].GetComponent<HatSpawn>().moveRight();

    }

    public void ChangeHatLeft()
    {

        characters[index].GetComponent<HatSpawn>().moveLeft();

    }*/

/*    public void SpawnCharacter()
    {
*//*        spawnedPlayerTransform = GameObject.Find("LocalPlayer").transform;

        CharacterController characterController = spawnedPlayerTransform.GetComponent<CharacterController>();
        characterController.enabled = false;
        characters[index].transform.position = spawnedPlayerTransform.position;
        spawnedPlayerTransform.rotation = characters[index].transform.rotation;
        characterController.enabled = true;
        spawnedPlayerTransform.GetComponent<NetworkObject>().SpawnAsPlayerObject(NetworkManager.Singleton.LocalClientId, true);


        characters[index].GetComponent<NetworkObject>().Spawn();
        characters[index].transform.parent = spawnedPlayerTransform;*//*
    }*/

    

/*    public void PaintCharacter()
    {

        characters[index].GetComponent<SlimeParticleColor>().StopPaint();
        characters[index].GetComponent<SlimeMaterialColor>().Paint();

    }


    public void PaintParticle()
    {

        characters[index].GetComponent<SlimeMaterialColor>().StopPaint();
        characters[index].GetComponent<SlimeParticleColor>().Paint();

    }

    public void StopChange()
    {

        characters[index].GetComponent<SlimeParticleColor>().StopPaint();
        characters[index].GetComponent<SlimeMaterialColor>().StopPaint();

    }

    public void DisableCharacters()
    {

        foreach (GameObject character in characters)
        {
            character.transform.gameObject.SetActive(false);
        }


    }

    public void ChangeFace(int faceIndex)
    {

        characters[index].GetComponent<SlimeFace>().ChangeFace(faceIndex);

    }

    public void ChangeParticles(int particlesIndex)
    {

        characters[index].GetComponent<SlimeParticle>().ChangeParticles(particlesIndex);

    }*/

    

    

