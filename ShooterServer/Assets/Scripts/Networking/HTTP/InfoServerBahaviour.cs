using PacketManager;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InfoServerBahaviour : MonoBehaviour
{
    public int port;
    public TcpListener infoServer;
    public Server gameServer;

    private async void Start()
    {
        gameServer = FindObjectOfType<ServerBehaviour>().server;
        infoServer = TcpListener.Create(port);
        infoServer.Start();
        GetRequest();
        Debug.Log("Info server started");
    }

    private void OnDestroy()
    {
        infoServer.Stop();
    }

    private async void GetRequest()
    {
        try
        {
            var client = await infoServer.AcceptTcpClientAsync();
            Answer(client);
        }
        catch { }
    }

    private void Answer(TcpClient client)
    {
        Debug.Log($"Server info sended to :{client.Client.RemoteEndPoint}");
        var answer = new ServerInfo(gameServer.maxPlayers, gameServer.players.GetPlayerCount(), 0);
        var jsonAnswer = JsonUtility.ToJson(answer);
        var byteAnswer = Encoding.UTF8.GetBytes(jsonAnswer);

        client.GetStream().WriteBytes(byteAnswer);
        client.Close();
        GetRequest();
    }
}
