using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SignalRClient Instance;
    public readonly object _lock = new object();
    [SerializeField] private GameObject lobbyPanel;
    public List<GameObject> playerList = new List<GameObject>();
    [SerializeField] private List<GameObject> playerPrefabs = new List<GameObject>();
    public static GameManager InstanceGM { get; private set; }

    private void Awake()
    {
        if (InstanceGM != null && InstanceGM != this)
        {
            Destroy(this);
        }
        else
        {
            InstanceGM = this;
        }
    }

    private void Start()
    {
        Instance = SignalRClient.Instance;
    }

    void Update()
    {
        if (Instance.triggerGameStart)
        {
            Debug.Log("15");
            int index = 0;
            foreach (string key in Instance.gameRoomList.Keys)
            {
                foreach (string user in Instance.gameRoomList[key])
                {
                    if (user.Equals(Instance.username)) // kendi araci olusacak
                    {
                        Debug.Log("16");
                        lobbyPanel.SetActive(false);
                        GameObject player = Instantiate(playerPrefabs[index]);
                        player.name = Instance.username;
                        player.GetComponent<Player>().isMine = true;
                        player.GetComponent<Player>().username = Instance.username;
                        index++;
                    }
                    else if (!user.Equals("")) // diger araclar olusacak
                    {
                        Debug.Log("17");
                        GameObject player = Instantiate(playerPrefabs[index]);
                        player.name = user;
                        player.GetComponent<Player>().username = user;
                        playerList.Add(player);
                        index++;
                    }
                }
            }

            Instance.triggerGameStart = false;
        }

        foreach (GameObject player in playerList)
        {
            Debug.Log("u1 player = " + player.name);
            foreach (string key in Instance.positions.Keys)
            {
                Debug.Log("u2 key = " + key);
                if (key.Equals(player.name))
                {
                    Debug.Log("u3 player = " + player.name);
                    player.transform.position = new Vector3(Instance.positions[key], -3.4628f, 0f);
                }
                else
                {
                    Debug.Log($"u4 Mismatch: key = {key}, player.name = {player.name}");
                }
            }
        }
    }
}