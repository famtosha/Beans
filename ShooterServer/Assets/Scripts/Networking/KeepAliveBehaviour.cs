using UnityEngine;
using PacketManager;

public class KeepAliveBehaviour : MonoBehaviour
{
    public Timer checkCD = new Timer(5);

    private ServerBehaviour server;

    private void Start()
    {
        server = FindObjectOfType<ServerBehaviour>();
    }

    private void Update()
    {
        if (checkCD.isReady)
        {
            foreach (var client in server.server.players.players)
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