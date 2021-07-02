using PacketManager;
using System.Linq;
using UnityEngine;

public class WeaponServerRequiester : MonoBehaviour
{
    private ClientBehaviour client;

    private void Awake()
    {
        client = FindObjectOfType<ClientBehaviour>();
    }

    public void DealDamage(GameObject[] hits, int damage)
    {
        hits = hits.Where(x => x.HasComponent<AnotherPlayerBehaviour>()).ToArray();
        var hitsID = hits.Select(x => x.GetComponent<AnotherPlayerBehaviour>().id).ToList();
        var unicHitIDs = hitsID.Distinct().ToList();

        foreach (var unicID in unicHitIDs)
        {
            int totalDamage = 0;
            foreach (var s in hitsID)
            {
                if (s == unicID) totalDamage += damage;
            }
            client.WritePacket(new DealDamgePacket(client.id, unicID, totalDamage));
        }
    }

    public void DealDamage(GameObject hit, int damage)
    {
        if (hit.TryGetComponent(out AnotherPlayerBehaviour anotherPlayer))
        {
            client.WritePacket(new DealDamgePacket(client.id, anotherPlayer.id, damage));
        }
    }

    public void ShotShoot(Vector3[] hits)
    {
        client.WritePacket(new ShootPacket(client.id, hits));
    }

    public void ShotShoot(Vector3 hit)
    {
        client.WritePacket(new ShootPacket(client.id, hit));
    }

    public void OnChangeWeapon(int weaponSlot)
    {
        client.WritePacket(new ChangeWeaponPacket(client.id, weaponSlot));
    }

    public void Reload()
    {
        client.WritePacket(new ReloadWeaponPacket(client.id));
    }

    public void SpawnObject(int assetID, Vector3 position, Vector3 scale, Quaternion rotation)
    {
        client.WritePacket(new SpawnSynchronizedObjectRequestPacket(assetID, position, scale, rotation));
    }
}
