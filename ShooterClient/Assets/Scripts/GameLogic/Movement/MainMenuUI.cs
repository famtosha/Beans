using UnityEngine;
using TMPro;
using System.Net;
using System.Net.Sockets;

public class MainMenuUI : MonoBehaviour
{
    public TMP_InputField ipField;
    public TMP_InputField portField;
    public TMP_InputField nameField;

    public TMP_Text serverInfo;

    public TextMeshProUGUI invalidValueField;

    public void ConnectToServer()
    {
        if (TryParseIPWithLog(out var ip))
        {
            ip.Port++;
            if (InfoServerRequester.TryGetServerInfo(ip, out _)) SceneLoader.instance.LoadGameScene(ip, nameField.text);
            ip.Port--;
        }
        else
        {
            ShowServerInfo("Offline");
        }
    }

    public void GetServerInfo()
    {
        if (TryParseIPWithLog(out var ip))
        {
            ip.Port++;
            string serverInfo = "";
            if (InfoServerRequester.TryGetServerInfo(ip, out var info))
            {
                serverInfo = "Online " + info.ToString();
            }
            else
            {
                serverInfo = "Offline";
            }
            ShowServerInfo(serverInfo);
        }
    }

    private void ShowServerInfo(string info)
    {
        serverInfo.text = info;
    }

    private bool TryParseIPWithLog(out IPEndPoint ip)
    {
        if (TryParseIP(out ip))
        {
            return true;
        }
        else
        {
            invalidValueField.text = "Wronge format!";
            return false;
        }
    }

    private bool TryParseIP(out IPEndPoint ip)
    {
        bool result;
        try
        {
            var address = IPAddress.Parse(ipField.text.RemoveTMPChars());
            var port = int.Parse(portField.text.RemoveTMPChars());
            ip = new IPEndPoint(address, port);
            result = true;
        }
        catch
        {
            ip = null;
            result = false;
        }
        return result;
    }
}
