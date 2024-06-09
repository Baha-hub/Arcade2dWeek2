using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private List<GameObject> buttonList = new List<GameObject>();
    [SerializeField] private Transform lobbyTransform;
    //private TcpClientScript Instance;
    private int roomCount;

    /*private void Start()
    {
        Instance = TcpClientScript.Instance;
    }*/

    public void RoomCreate()
    {
        //Instance.SendMessageToServer("CREATEROOM " + Instance.username);
        
        foreach (GameObject temp in buttonList)
        {
            temp.GetComponent<Button>().interactable = false;
        }
    }
    
    public void RoomQuit()
    {
        //Instance.SendMessageToServer("LEAVEROOM " + Instance.username);
    }

    public void JoinRoom(string roomName)
    {
        //Instance.SendMessageToServer("JOINROOM " + roomName);
        
        foreach (GameObject temp in buttonList)
        {
            temp.GetComponent<Button>().interactable = false;
        }
    }
    
    void Update()
    {
        /*if (roomCount != Instance.rooms.Count)
        {
            for (int i = 0; i < Instance.rooms.Count; i++)
            {
                GameObject button = Instantiate(buttonList[i], lobbyTransform);
                button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Instance.rooms[i];
                button.GetComponent<Button>().onClick.AddListener(delegate{JoinRoom(button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);} );       
            }
            roomCount = Instance.rooms.Count;
        }*/
    }
}
