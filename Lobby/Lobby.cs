/// <summary>
/// Lobby - Server Lobby Management
/// 
/// Manages lobby creation, management, and player joining on the server side.
/// Handles Unity Services integration for lobbies and authentication.
/// 
/// Features:
/// - Lobby creation and configuration
/// - Player management in lobbies
/// - Lobby data persistence
/// - Authentication integration
/// - Unity Services Lobbies API
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.IO.LowLevel.Unsafe;

public class Lobby : MonoBehaviour
{
    private Unity.Services.Lobbies.Models.Lobby hostLobby;
    private Unity.Services.Lobbies.Models.Lobby joinedLobby;
    private float hearBeatTimer;
    QueryFilter Filters;
    private string playerName;

    // Our initialization and authentication functions will be called async to avoid freezes.
    public async void SignIn()
    {
        await UnityServices.InitializeAsync();

        playerName = "Player" + UnityEngine.Random.Range(10, 99);

        // Signing in our player
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed In" + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Creating our lobby asynchronously.
    public async void CreateLobby(string lobbyName = "MyLobby", int maxPlayers = 4, bool isPrivate = true)
    {
        try
        {
            // Setting lobby to private or public.
            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
            {
                IsPrivate = isPrivate,

                // Defining data of our player.
                Player = new Player
                {
                    // Each argument will be dictionary containing key, PlayerDataObject
                    Data = new Dictionary<string, PlayerDataObject>
                    {
                        // Our player name will be visible to everyone in lobby.
                        // VisibilityOptions of Member will be visibile to lobby.
                        // VisibilityOptions of Member will be visibile outside lobby.
                        { "PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, playerName) }
                    }
                },

                // Defining data of our lobby.
                // Each argument will be dictionary containing key, DataObject
                Data = new Dictionary<string, DataObject>
                {

                    {"GameMode", new DataObject(DataObject.VisibilityOptions.Public, "FreeRoam") }
                }

            };

            // Takes arguments of lobby name and max number of players.
            Unity.Services.Lobbies.Models.Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, createLobbyOptions);


            Debug.Log("Created Lobby! " + lobby.Name + " " + lobby.MaxPlayers);

            // Storing our hostLobby for future updates.
            hostLobby = lobby;
            joinedLobby = lobby;

            // Getting list of players in hostLobby.
            PrintPlayers(hostLobby);

        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    // Getting all our lobbies.
    public async void ListLobbies()
    {
        try
        {
            // Filtering lobbies to get only that have available slots.
            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions
            {
                Count = 25,
                Filters = new List<QueryFilter> {
                    // Filter by OpOptions argument.
                    new QueryFilter(QueryFilter.FieldOptions.AvailableSlots, "0", QueryFilter.OpOptions.GT),

                    //Further filters fopr lobby data.
                    //new QueryFilter(QueryFilter.FieldOptions.S1, "FreeRoam", QueryFilter.OpOptions.EQ)
                },
                Order = new List<QueryOrder>
                {
                    new QueryOrder(false, QueryOrder.FieldOptions.Created)
                }
            };

            // Sending our filter and getting lobbies.
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync(queryLobbiesOptions);

            Debug.Log("Lobbies found: " + queryResponse.Results.Count);

            foreach (Unity.Services.Lobbies.Models.Lobby lobby in queryResponse.Results)
            {
                Debug.Log(lobby.Name + " " + lobby.MaxPlayers + " " + lobby.Data["GameMode"].Value);
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    public async void JoinLobby()
    {
        try
        {
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();

            // We can use JoinLobbyByCodeOptions to pass our player data when joining.
            JoinLobbyByCodeOptions joinLobbyByCodeOptions = new JoinLobbyByCodeOptions
            {
                Player = GetPlayer()
            };

            // Joining first lobby by it's id.
            joinedLobby = await Lobbies.Instance.JoinLobbyByCodeAsync(queryResponse.Results[0].Id, joinLobbyByCodeOptions);

            PrintPlayers(joinedLobby);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    public async void JoinLobbyByCode(string lobbyCode)
    {
        try
        {
            // We can use JoinLobbyByCodeOptions to pass our player data when joining.
            JoinLobbyByCodeOptions joinLobbyByCodeOptions = new JoinLobbyByCodeOptions
            {
                Player = GetPlayer()
            };

            // Joining lobby with a code and sending player data;
            joinedLobby = await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyCode, joinLobbyByCodeOptions);

            Debug.Log("Joined Lobby with code" + lobbyCode);
            PrintPlayers(joinedLobby);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    public async void QuickJoinLobby()
    {
        try
        {
            // We can use QuickJoinLobbyOptions to pass our player data when joining.
            QuickJoinLobbyOptions quickJoinLobbyOptions = new QuickJoinLobbyOptions
            {
                Player = GetPlayer()
            };

            // Quick joining lobby.
            joinedLobby = await Lobbies.Instance.QuickJoinLobbyAsync(quickJoinLobbyOptions);
            PrintPlayers(joinedLobby);

        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    public void PrintPlayers(Unity.Services.Lobbies.Models.Lobby lobby)
    {
        // Accessing lobby data.


        // Data takes index of data key.
        Debug.Log("Players in lobby" + lobby.Name + " " + lobby.Data["GameMode"].Value);

        foreach (Player player in lobby.Players)
        {
            // Accessing player data which takes index of data key.
            Debug.Log(player.Id.ToString() + " " + player.Data["PlayerName"].Value);
        }
    }

    private Player GetPlayer()
    {
        return new Player
        {
            Data = new Dictionary<string, PlayerDataObject>
            {
                // Our player name will be visible to everyone in lobby.
                { "PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, playerName) }
            }
        };
    }

    // Updating lobby data
    public async void UpdateLobbyGameMode(string gameMode)
    {
        try
        {
            hostLobby = await Lobbies.Instance.UpdateLobbyAsync(hostLobby.Id, new UpdateLobbyOptions
            {
                Data = new Dictionary<string, DataObject> {
                    { "GameMode", new DataObject(DataObject.VisibilityOptions.Public, gameMode)}
                }
            });
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    // Updating player data
    public async void UpdatePlayerName(string playerName)
    {
        try
        {
            await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId, new UpdatePlayerOptions
            {
                Data = new Dictionary<string, PlayerDataObject> {
                    { "PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, playerName) }
                }
            });
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }


    public async void LeaveLobby()
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    public async void KickPlayer()
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
}
