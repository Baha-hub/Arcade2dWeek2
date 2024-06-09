using System;
using Microsoft.AspNetCore.SignalR.Client;
using UnityEngine;
using TMPro;

public class LoginButton : MonoBehaviour
{
    private string username, password;
    [SerializeField] private TMP_InputField usernameText, passwordText;
    private SignalRClient _tcpClientScript;
    [SerializeField] private GameObject loginPanel,lobbyPanel;

    private void Awake()
    {
        _tcpClientScript = SignalRClient.Instance;
    }

    public async void LoginButtonAction()
    {
        username = usernameText.text;
        password = passwordText.text;

        string temp = "LOGIN " + username + " " + password;
        _tcpClientScript.SendMessageToServer(temp);
        
    }
    public void RegisterButtonAction()
    {
        username = usernameText.text;
        password = passwordText.text;

        string temp = "REGISTER " + username + " " + password;
        _tcpClientScript.SendMessageToServer(temp);
    }

    private void Update()
    {
        if (_tcpClientScript.isLogined)
        {
            
            loginPanel.SetActive(false);
            lobbyPanel.SetActive(true);
            _tcpClientScript.isLogined = false;
        }
    }
}