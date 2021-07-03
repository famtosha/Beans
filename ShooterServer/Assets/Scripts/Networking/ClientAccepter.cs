using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;

public class ClientAccepter
{
    private Server _server;
    private Action<TcpClient> _acceptCallback;

    public ClientAccepter(Server server, Action<TcpClient> acceptCallback)
    {
        _server = server;
        _acceptCallback = acceptCallback;
    }

    public async void StartListening()
    {
        var client = await _server.tcpServer.AcceptTcpClientAsync();
        AcceptClient(client);
    }

    public void AcceptClient(TcpClient client)
    {
        _acceptCallback(client);
        StartListening();
    }
}
