using UnityEngine;
using Zenject;

public class Map : MonoBehaviour
{
    public MapData currentMap { get; set; }
    public SynchronizedObjectList objectList { get; set; }

    [SerializeField] private SynchronizedObjectsFacotry _factory;
    [SerializeField] private MapData _startMap;
    private GameObject _rootObject;
    private ServerBehaviour _server;

    [Inject]
    private void Construct(ServerBehaviour serverBehaviour)
    {
        _server = serverBehaviour;
    }

    private void Start()
    {
        SpawnMap();
        objectList = new SynchronizedObjectList(_server.server, _rootObject, _factory);
    }

    public Vector3 GetSpawnPoint()
    {
        return currentMap.GetSpawnPoint().position;
    }


    private void SpawnMap()
    {
        currentMap = ScriptableObject.Instantiate(_startMap);
        _rootObject = Instantiate(currentMap.prefub);
    }
}