using TMPro;
using UnityEngine;

public class ActiveUsers : MonoBehaviour
{
    private TextMeshProUGUI _textMeshProUGUI;
    private SignalRClient Instance;

    private void Awake()
    {
        
        Instance = SignalRClient.Instance;
    }

    private void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Instance.someoneLogOrLeave)
        {
            _textMeshProUGUI.text = "";
            foreach (var item in Instance.userList)
            {
                _textMeshProUGUI.text += item + "\n";
            }
            Instance.someoneLogOrLeave = false;
        }
        
    }
}
