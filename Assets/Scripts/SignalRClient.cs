using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using UnityEngine;

public class SignalRClient : MonoBehaviour
{
    public HubConnection connection;
    public bool isLogined,someoneLogOrLeave, newMessage, listRooms, listJoins, triggerGameStart;
    public List<string> userList = new List<string>();
    public string messages, username, lastLoginRoom, lastLoginUsername;
    public Dictionary<string, List<string>> rooms = new Dictionary<string, List<string>>();
    public Dictionary<string, bool> readyList = new Dictionary<string, bool>();
    public Dictionary<string, List<string>> gameRoomList = new Dictionary<string, List<string>>();
    public string currentRoomName, removedRoom;
    public Dictionary<string, float> positions = new Dictionary<string, float>();
    private GameManager GMInstance = GameManager.InstanceGM;


    public static SignalRClient Instance { get; private set; }

    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
        } 
        else 
        { 
            Instance = this; 
        } 
        
    }
    async void Start()
    {
        connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5000/chatHub")
            .Build();

        connection.On<string, Dictionary<string, List<string>>>("LOGIN_TRUE",  (username, rooms) =>
        {
            this.username = username;
            this.rooms = rooms;
            isLogined = true;
            listRooms = true;
        });

        connection.On<List<string>>("USER_LIST", (userList) =>
        {
            this.userList = userList;
            someoneLogOrLeave = true;
        });

        connection.On<string>("LOGIN_DENIED", (message) =>
        {
            Debug.Log($"Server: {message}");
        });

        connection.On<string>("LOGIN_FALSE", (message) =>
        {
            Debug.Log($"Server: {message}");
        });

        connection.On<string>("REGISTER_TRUE", (message) =>
        {
            Debug.Log($"Server: {message}");
        });

        connection.On<string>("REGISTER_FALSE", (message) =>
        {
            Debug.Log($"Server: {message}");
        });

        connection.On<string>("MESSAGE_TRUE", (message) =>
        {
            messages = message;
            newMessage = true;
            Debug.Log(message);
        });
        
        connection.On<Dictionary<string, bool>>("READY_LIST", (message) =>
        {
            readyList = message;
            listJoins = true;
        });
        
        connection.On<Dictionary<string, List<string>>>("START_GAME_TRUE", (message) =>
        {
            gameRoomList = message;
            triggerGameStart = true;
        });
        
        connection.On<Dictionary<string, float>>("SET_POSITION", (message) =>
        {
                positions = message;
                Debug.Log("pozisyon = " + positions["u"]);
                Debug.Log("pozisyon = " + positions["b"]);
        });
        
        connection.On<Dictionary<string, List<string>>, string>("CREATEROOM_TRUE", (message, roomName) =>
        {
            rooms = message;
            if (username.Equals(roomName))
            {
                currentRoomName = roomName;
            }
            listRooms = true;
            listJoins = true;
        });
            
        connection.On<string>("CREATEROOM_FALSE", (message) =>
        {
            Debug.Log("oda zaten var veya kotu bisiler oldu oda olamadi");
        });

        connection.On<Dictionary<string, List<string>>, string, string>("JOINROOM_TRUE", (message, roomName, loggedUsername) =>
        {
            Debug.Log("loggedusername = "+ loggedUsername);
            rooms = message;
            lastLoginUsername = loggedUsername;
            if (lastLoginUsername.Equals(username))
            {
                currentRoomName = roomName;
                Debug.Log("current rrom = " + currentRoomName);
            }
            listJoins = true;
        });

        connection.On<string>("JOINROOM_FALSE", (message) =>
        {
            Debug.Log("oda yok veya zaten odadasiniz");
        });

        connection.On<Dictionary<string, List<string>>, string>("REMOVEROOM_TRUE", (message, removedRoom) =>
        {
            rooms = message;
            listRooms = true;
            listJoins = true;
        });

        connection.On<Dictionary<string, List<string>>>("LEAVEROOM_TRUE", (message) =>
        {
            rooms = message;
            listRooms = true;
            listJoins = true;
        });
        
        connection.On<string, float, float>("SHOOT_EXECUTED", (username, positionX, positionY) =>
        {
        });

        connection.On<string>("LEAVEROOM_FALSE", (message) =>
        {
            Debug.Log("odadan cikilamadi");
        });

        connection.On<string>("KONTROL", (message) =>
        {
            Debug.Log($"Server: {message}");
        });

        connection.On<string>("HKONTROL", (message) =>
        {
            Debug.Log($"Server: {message}");
        });

        connection.On<string>("FALSE", (message) =>
        {
            Debug.Log($"Server: {message}");
        });

        try
        {
            await connection.StartAsync();
            Debug.Log("Connected to the server.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Connection failed: {ex.Message}");
        }
    }

    public async Task SendMessageToServer(string message)
    {
        await connection.InvokeAsync("TakeMessage", message);
    }

    private async void OnApplicationQuit()
    {
        if (connection != null)
        {
            await connection.StopAsync();
            await connection.DisposeAsync();
        }
    }
}