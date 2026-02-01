/// <summary>
/// TestRelay - Unity Relay Testing & Integration
/// 
/// Testing and debugging utility for Unity Relay networking integration.
/// Handles relay allocation and connection testing.
/// 
/// Features:
/// - Relay server allocation
/// - Connection testing
/// - Transport configuration
/// - Debugging utilities
/// 
/// Dependencies: Unity Relay, Netcode Transport
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestRelay : MonoBehaviour
{
    public static TestRelay Instance;

    private static int nextAllocationId = 1;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public async Task<string> CreateRelay()
    {
        try
        {
            int uniqueAllocationId = nextAllocationId; // Get a unique allocation ID
            nextAllocationId++; // Increment for the next client

            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            Debug.Log(joinCode);

            /*            NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
                            allocation.RelayServer.IpV4,
                            (ushort)allocation.RelayServer.Port,
                            allocation.AllocationIdBytes,
                            allocation.Key,
                            allocation.ConnectionData
                        );*/

            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartHost();


            NetworkManager.Singleton.SceneManager.LoadScene("CharacterSelectionScene", LoadSceneMode.Single);


            return joinCode;
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e.Message);
            return null;
        }
    }

    public async void JoinRelay(string joinCode)
    {
        try
        {
            Debug.Log("Joining Relay with" + joinCode);
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartClient();
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e.Message);
        }
    }

    public async void LeaveRelay()
    {
        try
        {

            Debug.Log("Left Relay");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error leaving relay: {e.Message}");
        }
    }
}
