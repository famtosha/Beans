using System;
using UnityEngine;

public class ServerBehaviour : MonoBehaviour
{
    public ClientAccepter clientAccepter;
    public SynchronizedObjectList synchronizedObjectList;
    public Map map;
    public Server server;
    public int port;

    private void Awake()
    {
        try
        {
            server = new Server(port, clientAccepter, map);
            clientAccepter.acceptCallback = server.ConnectClient;
            clientAccepter.StartListening();
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
