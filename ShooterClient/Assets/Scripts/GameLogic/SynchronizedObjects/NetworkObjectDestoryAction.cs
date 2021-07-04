using UnityEngine;

[RequireComponent(typeof(StaticLevelObject))]
public class NetworkObjectDestoryAction : MonoBehaviour
{
    [SerializeField] private ParticleSystem _destroyParticle;
    private StaticLevelObject _levelObject;

    private void Awake()
    {
        _levelObject = GetComponent<StaticLevelObject>();
    }

    private void Start()
    {
        _levelObject.Destroyed += OnNetworkObjectDestoryed;
    }

    private void OnNetworkObjectDestoryed()
    {
        _levelObject.Destroyed -= OnNetworkObjectDestoryed;
        Instantiate(_destroyParticle, transform.position, Quaternion.identity);
    }
}