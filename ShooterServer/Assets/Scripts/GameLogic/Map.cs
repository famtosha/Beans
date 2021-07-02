using UnityEngine;

public class Map : MonoBehaviour
{
    public MapData startMap;
    public SynchronizedObjectList objectList;
    [SerializeField] private SynchronizedObjectsFacotry _factory;
    private MapData currentMap;
    private GameObject _rootObject;

    private void Start()
    {
        SpawnMap();
        objectList = new SynchronizedObjectList(FindObjectOfType<ServerBehaviour>().server, _rootObject, _factory);
    }

    public Vector3 GetSpawnPoint()
    {
        return currentMap.GetSpawnPoint().position;
    }


    private void SpawnMap()
    {
        currentMap = ScriptableObject.Instantiate(startMap);
        _rootObject = Instantiate(currentMap.prefub);
    }
}