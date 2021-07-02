using PacketManager;
using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System.Text;

public class InfoServer
{
    public int port;
    public TcpListener infoServer;
    public Server gameServer;

    public InfoServer(int port, Server gameServer)
    {
        this.port = port;
        this.gameServer = gameServer;
        infoServer = TcpListener.Create(port);
        infoServer.Start();
        WaitRequest();
    }

    ~InfoServer()
    {
        infoServer.Stop();
    }

    private void Answer(TcpClient client)
    {
        Debug.Log($"connected to info server:{client.Client.RemoteEndPoint}");
        var answer = new ServerInfo(gameServer.maxPlayers, gameServer.players.players.Length, 0);
        var jsonAnswer = JsonUtility.ToJson(answer);
        Debug.Log("Info:" + jsonAnswer);
        var byteAnswer = Encoding.UTF8.GetBytes(jsonAnswer);

        client.GetStream().WriteBytes(byteAnswer);
        Debug.Log($"Sended info");
        client.Close();
    }

    private void AnswerRequiest(IAsyncResult result)
    {
        var context = infoServer.EndAcceptTcpClient(result);
        Answer(context);
        WaitRequest();
    }

    private void WaitRequest()
    {
        infoServer.BeginAcceptTcpClient(AnswerRequiest, null);
        Debug.Log("Listening...");
    }
}
