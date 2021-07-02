using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;

public class ClientAccepter : MonoBehaviour
{
    public ServerBehaviour server;
    public Action<TcpClient> acceptCallback;

    public async void StartListening()
    {
        var client = await server.server.server.AcceptTcpClientAsync();
        AcceptClient(client);
    }

    public void AcceptClient(TcpClient client)
    {
        acceptCallback(client);
        StartListening();
    }
}
