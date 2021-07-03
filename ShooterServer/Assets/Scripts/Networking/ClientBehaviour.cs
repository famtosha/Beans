using UnityEngine;
using Zenject;

public class ClientBehaviour : MonoBehaviour
{
    public Client client { get; set; }

    private ServerBehaviour _serverBehaviour;

    [Inject]
    private void Construct(ServerBehaviour serverBehaviour)
    {
        _serverBehaviour = serverBehaviour;
    }

    public void DealDamage(int damage)
    {
        _serverBehaviour.server.players.DealDamage(0, client.id, damage);
    }
}
