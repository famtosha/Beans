using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapFactory", menuName = "Factories/MapFactory")]
public class MapFactory : ScriptableObject
{
    [SerializeField] private List<Map> _maps = new List<Map>();



    public GameObject Create(int mapID)
    {
        return Object.Instantiate(_maps[mapID].prefub, Vector3.zero, Quaternion.identity);
    }
}
