using UnityEngine;
using PacketManager;

public class PlayerPostionUpdater : MonoBehaviour
{
    public Timer sendCD = new Timer(0.5f);
    private ServerBehaviour server;

    private void Start()
    {
        server = FindObjectOfType<ServerBehaviour>();
    }

    private void Update()
    {
        sendCD.UpdateTimer(Time.deltaTime);
        if (sendCD.isReady)
        {
            SendNewPosition();
            sendCD.Reset();
        }
    }

    private void SendNewPosition()
    {
        if (server == null) Destroy(gameObject);
        foreach (var client in server.server.players.players)
        {
            if (client != null)
            {
                foreach (var anotherClient in server.server.players.players)
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