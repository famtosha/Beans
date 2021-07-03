using System;
using UnityEngine;
using Zenject;

public class ServerBehaviour : MonoBehaviour
{
    public Server server { get; set; }

    [SerializeField] private int port;
    private ClientAccepter _clientAccepter;
    private Map _map;

    [Inject]
    private void Construct(Map map)
    {
        _map = map;
    }

    private void Awake()
    {
        try
        {
            server = new Server(port, _map);
            _clientAccepter = new ClientAccepter(server, server.ConnectClient);
            _clientAccepter.StartListening();
            Log.Write($"Server started at port: {port}");
        }
        catch (Exception ex)
        {
            StartFallback(ex);
        }
    }

    private void StartFallback(Exception exception)
    {
        Log.WriteError($"Failed to start server: {exception}");
        Destroy(gameObject);
    }

    private void Update()
    {
        server.ReadPackets();
    }
}
