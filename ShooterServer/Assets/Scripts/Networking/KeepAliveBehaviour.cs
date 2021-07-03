using UnityEngine;
using PacketManager;
using Zenject;

public class KeepAliveBehaviour : MonoBehaviour
{
    public Timer checkCD = new Timer(5);

    private ServerBehaviour _server;

    [Inject]
    private void Construct(ServerBehaviour server)
    {
        _server = server;
    }

    private void Update()
    {
        if (checkCD.isReady)
        {
            foreach (var client in _server.server.players.players)
            {
                if (client != null)
                {
                    client.WritePacket(new KeepAlivePacket());
                }
            }
            checkCD.Reset();
        }
    }
}