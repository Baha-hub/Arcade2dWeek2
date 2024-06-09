using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomController : MonoBehaviour
{
    private SignalRClient Instance;
    [SerializeField] private List<GameObject> buttonList = new List<GameObject>();
    private List<GameObject> activeButtons = new List<GameObject>();
    [SerializeField] private Transform lobbyTransform;
    [SerializeField] private TextMeshProUGUI roomMembers;
    [SerializeField] private GameObject createButton, roomPanel, startBtn;

    private void Start()
    {
        Instance = SignalRClient.Instance;
    }

    public void CreateRoomButton()
    {
        /*ResetRoomButtons();
        roomPanel.SetActive(false);*/
        /*foreach (string roomKey  in Instance.rooms.Keys)
        {
            if (Instance.rooms[roomKey].Contains(Instance.username))
            {
                
            }
        }*/
        LeaveRoom();
        Instance.SendMessageToServer("CREATEROOM");
    }

    public void JoinRoomButton(string roomName)
    {
        //button.transform.parent.parent.gameObject.SetActive(false);
        if (!roomName.Equals(Instance.username))
        {
            LeaveRoom();
            startBtn.SetActive(false);
            Instance.SendMessageToServer("JOINROOM "+ roomName);
        }
    }
    public void ReadyButton()
    {
        Instance.SendMessageToServer("READY");
    }

    private void ResetRoomButtons()
    {
        foreach (GameObject buttonGO in activeButtons)
        {
            buttonGO.SetActive(false);
        }
        activeButtons.Clear();
    }

    public void RoomsPanelReset()
    {
        roomPanel.SetActive(!roomPanel.activeSelf);
    }

    public void LeaveRoom()
    {
        Debug.Log("cikis yapilan oda = " + Instance.currentRoomName + "cikis yapan kisi = " + Instance.username);
        Instance.SendMessageToServer("LEAVEROOM " + Instance.currentRoomName);
    }
    private void Update()
    {
        if (Instance.listRooms)
        {
            ResetRoomButtons();
            int index = 0;
            foreach (string keys in Instance.rooms.Keys)
            {
                GameObject button = Instantiate(buttonList[index], lobbyTransform);
                activeButtons.Add(button);
                button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = keys;
                button.GetComponent<Button>().onClick.AddListener(delegate{JoinRoomButton(button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);} );
                index++;
            }

            Instance.listRooms = false;
        }
        if (Instance.listJoins)
        {
            foreach (string key in Instance.rooms.Keys)
            {
                    if (Instance.rooms[key].Contains(Instance.username))
                    {
                        roomMembers.text = "";
                        foreach (string text in Instance.rooms[key])
                        {
                            roomMembers.text += text;
                            foreach (string username in Instance.readyList.Keys)
                            {
                                if (username.Equals(text) && Instance.readyList[username])
                                {
                                    roomMembers.text +=" -> HAZIR\n";
                                }
                                else
                                {
                                    roomMembers.text +="\n";
                                }
                            }
                        }
                    }
            }
            Instance.listJoins = false;
        }
        
        
        
        /*if (Instance.roomRemoved)
        {
            if (Instance.currentRoomName.Equals(Instance.removedRoom))
            {
                roomPanel.SetActive(true);
                createButton.SetActive(true);
                leaveRoom.SetActive(false);
            }
        }
        if (Instance.newRoom)
        {
            int index = 0;
            if (Instance.rooms.Count == 0)
            {
                roomMembers.text = "";
            }
            foreach (string keys in Instance.rooms.Keys)
            {
                GameObject button = Instantiate(buttonList[index], lobbyTransform);
                activeButtons.Add(button);
                button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = keys;
                button.GetComponent<Button>().onClick.AddListener(delegate{JoinRoomButton(button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);} );
                index++;
            }
            Instance.newRoom = false;
            Instance.newJoin = true;
            if (Instance.username.Equals(Instance.currentRoomName))
            {
                ResetRoomButtons();
            }
        }

        if (Instance.closeRoomPanel)
        {
            RoomsPanelReset();
            Instance.closeRoomPanel = false;
        }

        if (Instance.newLeave)
        {
            roomPanel.SetActive(true);
            createButton.SetActive(true);
            roomMembers.text = "";
            Instance.newRoom = true;
            Instance.newJoin = true;
            leaveRoom.SetActive(false);
            Instance.newLeave = false;
        }
        if (Instance.newJoin)
        {
            foreach (string key in Instance.rooms.Keys)
            {
                if (key.Equals(Instance.lastLoginRoom))
                {
                    if (Instance.rooms[key].Contains(Instance.username))
                    {
                        roomMembers.text = "";
                        foreach (string text in Instance.rooms[key])
                        {
                            roomMembers.text += text + "\n";
                        }
                    }
                }
            }
            if (Instance.lastLoginUsername.Equals(Instance.username))
            {
                leaveRoom.SetActive(true);
                createButton.SetActive(false);
                roomPanel.SetActive(false);
            }
            
            if (Instance.rooms.ContainsKey(Instance.lastLoginRoom))
            {
                if (Instance.rooms[Instance.lastLoginRoom].Count == 0)
                {
                    roomMembers.text = "";
                }
            }
            
            
            Instance.newJoin = false;
        }*/
    }
}