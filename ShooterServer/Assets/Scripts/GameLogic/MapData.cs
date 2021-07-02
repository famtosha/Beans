using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "")]
public class MapData : ScriptableObject
{
    public int mapID;
    public GameObject prefub;

    public Transform GetSpawnPoint()
    {
        return prefub.GetComponentInChildren<SpawnPoint>().gameObject.transform;
    }
}
