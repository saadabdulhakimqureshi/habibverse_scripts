/// <summary>
/// HabibVerseFreeroamManager - Freeroam Game Mode Manager
/// 
/// Manages the freeroam exploration game mode without objectives.
/// Handles player spawning, game state, and mode-specific mechanics.
/// 
/// Features:
/// - Freeroam game initialization
/// - Player spawn management
/// - Freeroam-specific rules and mechanics
/// - Singleton access pattern
/// 
/// Dependencies: Netcode
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class HabibVerseFreeroamManager : NetworkBehaviour

{
    public static HabibVerseFreeroamManager Instance;
    [SerializeField] protected Transform playerPrefab;

    [SerializeField] protected List<Transform> librarySecondFloorSpawns;
    [SerializeField] protected List<Transform> tariqRafiSpawns;
    [SerializeField] protected List<Transform> informationCommonsSpawns;
    [SerializeField] protected List<Transform> tapalSpawns;
    [SerializeField] protected List<Transform> spawns;



    public void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {

            // When all clients have loaded this scene.
            NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += SceneManager_OnLoadEventCompleted;
        }
    }

    public void Update()
    {

    }

    public void SceneManager_OnLoadEventCompleted(string sceneName, UnityEngine.SceneManagement.LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        string gameMap = LobbyManager.Instance.GetGameMap();
        if (gameMap == LobbyManager.GameMap.LibrarySecondFloor.ToString())
        {
            spawns = librarySecondFloorSpawns;
        }
        else if (gameMap == LobbyManager.GameMap.Tapal.ToString())
        {
            spawns = tapalSpawns;
        }
        else if (gameMap == LobbyManager.GameMap.InformationCommons.ToString())
        {
            spawns = informationCommonsSpawns;
        }
        else if (gameMap == LobbyManager.GameMap.TariqRafi.ToString())
        {
            spawns = tariqRafiSpawns;
        }

        int i = 0;
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {

            Transform playerTransform = Instantiate(playerPrefab, spawns[i]);

            // Spawning player for a client id.
            playerTransform.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);

            playerTransform.GetComponent<PlayerController>().SetClientId(clientId);
            i++;
        }
    }








}
