using PacketManager;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class InfoServerBahaviour : MonoBehaviour
{
    public int port;

    private ServerBehaviour _gameServer;
    private TcpListener _infoServer;

    [Inject]
    private void Construct(ServerBehaviour gameServerBehaviour)
    {
        _gameServer = gameServerBehaviour;
    }

    private async void Start()
    {
        _infoServer = TcpListener.Create(port);
        _infoServer.Start();
        GetRequest();
        Debug.Log("Info server started");
    }

    private void OnDestroy()
    {
        _infoServer.Stop();
    }

    private async void GetRequest()
    {
        try
        {
            var client = await _infoServer.AcceptTcpClientAsync();
            Answer(client);
        }
        catch { }
    }

    private void Answer(TcpClient client)
    {
        var answer = new ServerInfo(_gameServer.server.maxPlayers, _gameServer.server.players.GetPlayerCount(), _gameServer.server.map.currentMap.mapID);
        var jsonAnswer = JsonUtility.ToJson(answer);
        var byteAnswer = Encoding.UTF8.GetBytes(jsonAnswer);

        client.GetStream().WriteBytes(byteAnswer);
        client.Close();
        GetRequest();
        Debug.Log($"Server info sended to :{client.Client.RemoteEndPoint}");
    }
}
