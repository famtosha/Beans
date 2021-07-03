using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(DynamicLevelObject))]
public class Missile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _selfExplodeDelay;
    [SerializeField] private float _radius;
    [SerializeField] private float _damage;

    private DynamicLevelObject _dynamicLevelObject;

    private void Awake()
    {
        _dynamicLevelObject = GetComponent<DynamicLevelObject>();
        transform.Rotate(90, 0, 0);
    }

    private void Update()
    {
        _selfExplodeDelay -= Time.deltaTime;
        Move();
        if (_selfExplodeDelay < 0) Explode();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    private void Move()
    {
        transform.position += transform.up * _speed * Time.deltaTime;
    }

    private void Explode()
    {
        //var explosionPosition = transform.position;
        //var nearObjects = Physics.OverlapSphere(explosionPosition, _radius)
        //    .Select(x => x.GetComponent<ClientBehaviour>())
        //    .Where(x => x != null)
        //    .Where(x => CanSeeObject(explosionPosition, x.transform))
        //    .ToList();
        var players = FindObjectOfType<ServerBehaviour>().server.players;
        foreach (var player in players.players)
        {
            if (player == null) continue;

            var distance = Vector3.Distance(transform.position, player.position);
            if (distance > _radius) continue;
            int damage = Mathf.RoundToInt(_damage - distance);

            players.DealDamage(0, player.id, damage);
        }
        _dynamicLevelObject.DestoryByServer();
    }

    private bool CanSeeObject(Vector3 source, Vector3 target)
    {
        return Physics.RaycastAll(source, target - source, Vector3.Distance(source, target)).Length == 0;
    }
}
