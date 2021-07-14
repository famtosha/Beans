using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(DynamicLevelObject))]
[RequireComponent(typeof(Rigidbody))]
public class Missile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _selfExplodeDelay;
    [SerializeField] private float _radius;
    [SerializeField] private float _damage;

    private Rigidbody _rb;

    private bool _isExploded = false;

    private DynamicLevelObject _dynamicLevelObject;

    private void Awake()
    {
        _dynamicLevelObject = GetComponent<DynamicLevelObject>();
        _rb = GetComponent<Rigidbody>();
        transform.Rotate(90, 0, 0);
        _rb.AddForce(transform.up * _speed);
    }

    private void Update()
    {
        _selfExplodeDelay -= Time.deltaTime;
        if (_selfExplodeDelay < 0) Explode();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isExploded) Explode();
    }

    private void Explode()
    {
        _isExploded = true;
        var explosionPosition = transform.position;
        var hitted = Physics.OverlapSphere(explosionPosition, _radius).ToList();
        var visible = hitted.Where(x => CanSeeObject(explosionPosition, x.transform.position)).ToList();
        var iDamagabel = visible.Select(x => x.GetComponent<IDamagable>()).ToList();
        iDamagabel = iDamagabel.Where(x => x != null).ToList();
        iDamagabel.ToList()
        .ForEach(x => x.ApplyDamage(1));

        var players = FindObjectOfType<ServerBehaviour>().server.players;
        foreach (var player in players.players)
        {
            if (player == null) continue;
            if (CalculateDamage(player.position, out var damage))
            {
                players.DealDamage(0, player.id, damage);
            }
        }

        _dynamicLevelObject.DestoryByServer();
    }

    private bool CalculateDamage(Vector3 targetPosition, out int damage)
    {
        var distance = Vector3.Distance(transform.position, targetPosition);
        damage = Mathf.RoundToInt(_damage - distance);
        if (distance > _radius) damage = 0;
        damage = Mathf.Clamp(damage, 0, damage);
        return damage > 0;
    }

    private bool CanSeeObject(Vector3 source, Vector3 target)
    {
        var hits = Physics.RaycastAll(source, target - source, Vector3.Distance(source, target));
        return hits.Length == 1;
    }
}
