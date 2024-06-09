using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] private TMP_InputField messageSendBox;
    private SignalRClient Instance;
    
    private void Start()
    {
        Instance = SignalRClient.Instance;
    }

    public void SendMessageToServer()
    {
        if (!messageSendBox.text.Equals(""))
        {
            Instance.SendMessageToServer("MESSAGE " + messageSendBox.text);
        }
        
    }

    private void Update()
    {
        if (Instance.newMessage)
        {
            _textMeshProUGUI.text += Instance.messages + "\n";
            Instance.newMessage = false;
        }
    }
}
