using UnityEngine;
using PacketManager;
using Zenject;

public class KeepAliveBehaviour : MonoBehaviour
{
    [SerializeField] private Timer _checkCD = new Timer(5);
    private ServerBehaviour _server;

    [Inject]
    private void Construct(ServerBehaviour server)
    {
        _server = server;
    }

    private void Update()
    {
        if (_checkCD.isReady)
        {
            foreach (var client in _server.server.players.players)
            {
                if (client != null)
                {
                    client.WritePacket(new KeepAlivePacket());
                }
            }
            _checkCD.Reset();
        }
    }
}