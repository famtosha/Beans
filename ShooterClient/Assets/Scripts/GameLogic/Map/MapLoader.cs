using UnityEngine;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private MapFactory _mapFactory;

    public void LoadMap(int mapID)
    {
        _mapFactory.Create(mapID);
    }
}
