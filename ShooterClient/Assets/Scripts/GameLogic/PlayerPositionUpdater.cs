using UnityEngine;
using PacketManager;

public class PlayerPositionUpdater : MonoBehaviour
{
    public Timer sendCD = new Timer(0.5f);

    public Transform position;
    public Transform rotation;

    private ClientBehaviour client;

    private void Start()
    {
        client = FindObjectOfType<ClientBehaviour>();
    }

    private void Update()
    {
        sendCD.UpdateTimer(Time.deltaTime);
        if (sendCD.isReady)
        {
            client.WritePacket(new PlayerTransformPacket(client.id, position.transform.position, rotation.transform.rotation));
            sendCD.Reset();
        }
    }
}
