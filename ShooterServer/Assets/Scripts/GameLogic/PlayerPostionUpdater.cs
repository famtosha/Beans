using UnityEngine;
using PacketManager;
using Zenject;

public class PlayerPostionUpdater : MonoBehaviour
{
    [SerializeField] private Timer _sendCD = new Timer(0.5f);
    private ServerBehaviour _server;

    private void Start()
    {
        _server = FindObjectOfType<ServerBehaviour>();
    }

    private void Update()
    {
        _sendCD.UpdateTimer(Time.deltaTime);
        if (_sendCD.isReady)
        {
            SendNewPosition();
            _sendCD.Reset();
        }
    }

    private void SendNewPosition()
    {
        if (_server == null) Destroy(gameObject);
        foreach (var client in _server.server.players.players)
        {
            if (client != null)
            {
                foreach (var anotherClient in _server.server.players.players)
                {
                    if (anotherClient != null && anotherClient.id != client.id)
                    {
                        client.WritePacket(new PlayerTransformPacket(anotherClient.id, anotherClient.position, anotherClient.rotation));
                    }
                }
            }
        }
    }
}