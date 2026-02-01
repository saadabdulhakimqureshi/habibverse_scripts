using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using Random = System.Random;

public class HabibVerseTreasureHuntManager : NetworkBehaviour

{
    public static HabibVerseTreasureHuntManager Instance;
    [SerializeField] protected Transform playerPrefab;
    [SerializeField] protected List<Transform> spawns;


    public Treasure[] path;
    public List<GameObject> treasurePaths;


    public bool huntStarted = false;

    public NetworkVariable<int> index;
    public NetworkVariable<int> randomNumber;
    public NetworkVariable<float> timer;
    public NetworkVariable<FixedString128Bytes> currentHint;

    public event EventHandler HintChanged;
    public event EventHandler GameWon;
    [SerializeField] float startTimer = 200f;

    public void Awake()
    {
        currentHint = new NetworkVariable<FixedString128Bytes>();
        index = new NetworkVariable<int>(0);
        randomNumber = new NetworkVariable<int>(0);
        timer = new NetworkVariable<float>(startTimer);

        Instance = this;
    }

    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            GenerateHuntServerRpc();

            // When all clients have loaded this scene.
            NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += SceneManager_OnLoadEventCompleted;
        }
    }

    public void SceneManager_OnLoadEventCompleted(string sceneName, UnityEngine.SceneManagement.LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {

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

    private void Update() {
        if (index.Value < path.Length)
        {
            path[index.Value].gameObject.SetActive(true);
        }
        if (index.Value > 0){
            path[index.Value-1].gameObject.SetActive(false);
        }
        if (index.Value == path.Length)
        {
            currentHint.Value = path[index.Value - 1].information;
        }
        if (currentHint.Value == "Time is up!")
        {
            treasurePaths[randomNumber.Value].SetActive(false);
        }


        ReduceTimerServerRpc();
    }

    [ServerRpc]
    private void ReduceTimerServerRpc()
    {
        timer.Value -= Time.deltaTime;
    }

    [ServerRpc]
    private void GenerateHuntServerRpc()
    {
        Random random = new Random();
        randomNumber.Value = random.Next(0, treasurePaths.Count);
        GenerateHuntClientRpc();
    }

    [ClientRpc] 
    private void GenerateHuntClientRpc()
    {
        Debug.Log("Treasure Trail Number " + randomNumber.Value);
        treasurePaths[randomNumber.Value].SetActive(true);
        path = treasurePaths[randomNumber.Value].GetComponentsInChildren<Treasure>();
        foreach (var treasure in path)
        {
            treasure.gameObject.SetActive(false);

        }

        path[0].gameObject.SetActive(true);
        SetHintServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetHintServerRpc()
    {
        currentHint.Value = path[index.Value].hint;
        SetHintClientRpc();
    }

    [ClientRpc]
    private void SetHintClientRpc()
    {
        Debug.Log(currentHint.Value);
        HintChanged?.Invoke(this, EventArgs.Empty);
    }

    [ServerRpc(RequireOwnership = false)]
    public void NextClueServerRpc()
    {
        index.Value = index.Value + 1;
        NextClueClientRpc();
    }

    [ClientRpc]
    public void NextClueClientRpc()
    {
        Debug.Log("Index " + index.Value);

        if (index.Value == path.Length)
        {
            WonServerRpc();
        }
        else
        {
            Debug.Log("Activating next clue");

            SetHintServerRpc();
        }
    }
    
    [ServerRpc(RequireOwnership = false)]
    public void WonServerRpc()
    {
        currentHint.Value = path[index.Value].information;
        WonClientRpc();
    }

    [ClientRpc]
    public void WonClientRpc()
    {
        HintChanged?.Invoke(this, EventArgs.Empty);
    }

    [ServerRpc(RequireOwnership = false)]
    public void LoseServerRpc()
    {
        currentHint.Value = "Time is up!";
        treasurePaths[randomNumber.Value].SetActive(false);
        LoseClientRpc();
    }

    [ClientRpc]
    public void LoseClientRpc()
    {
        HintChanged?.Invoke(this, EventArgs.Empty);
        treasurePaths[randomNumber.Value].SetActive(false);
    }
}
