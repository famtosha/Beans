using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "")]
public class Map : ScriptableObject
{
    public int mapID;
    public GameObject prefub;
    public Transform spawnPoint => prefub.GetComponentInChildren<SpawnPoint>().gameObject.transform;
}
