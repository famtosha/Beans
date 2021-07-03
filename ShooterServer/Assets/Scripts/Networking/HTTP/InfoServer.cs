using PacketManager;
using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System.Text;

public class InfoServer
{
    private int _port;
    private TcpListener _infoServer;
    private Server _gameServer;

    public InfoServer(int port, Server gameServer)
    {
        _port = port;
        _gameServer = gameServer;
        _infoServer = TcpListener.Create(_port);
        _infoServer.Start();
        WaitRequest();
    }

    ~InfoServer()
    {
        _infoServer.Stop();
    }

    private void Answer(TcpClient client)
    {
        Debug.Log($"connected to info server:{client.Client.RemoteEndPoint}");
        var answer = new ServerInfo(_gameServer.maxPlayers, _gameServer.players.players.Length, 0);
        var jsonAnswer = JsonUtility.ToJson(answer);
        Debug.Log("Info:" + jsonAnswer);
        var byteAnswer = Encoding.UTF8.GetBytes(jsonAnswer);

        client.GetStream().WriteBytes(byteAnswer);
        Debug.Log($"Sended info");
        client.Close();
    }

    private void AnswerRequiest(IAsyncResult result)
    {
        var context = _infoServer.EndAcceptTcpClient(result);
        Answer(context);
        WaitRequest();
    }

    private void WaitRequest()
    {
        _infoServer.BeginAcceptTcpClient(AnswerRequiest, null);
        Debug.Log("Listening...");
    }
}
