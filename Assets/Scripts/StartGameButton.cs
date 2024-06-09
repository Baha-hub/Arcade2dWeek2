using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    private SignalRClient Instance;
    private GameManager InstanceGM;
    private void Start()
    {
        Instance = SignalRClient.Instance;
        InstanceGM = GameManager.InstanceGM;
    }
    public void StartGameBtn()
    {
        if (Instance.currentRoomName.Equals(Instance.username)) // bir odadaysa ve odanin hostuysa
        {
            Debug.Log("11");
            int readyPlayerCount = 0;
            foreach (string key in Instance.rooms.Keys)
            {
                foreach (string username in Instance.rooms[key])
                {
                    Debug.Log("12");
                    if (Instance.readyList[username])
                    {
                        Debug.Log("13");
                        readyPlayerCount++;
                    }
                }
                Debug.Log("14");
                if (readyPlayerCount == Instance.rooms[key].Count)
                {
                    Debug.Log("oyun baslatildi");
                    Instance.SendMessageToServer("START_GAME " + key);
                }
            }
            
            
        }
    }
}
