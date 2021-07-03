using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "SOLF", menuName = "Factories/SOLF")]
public class SynchronizedObjectsFacotry : ScriptableObject, ISynchronizedObjectsFacotry
{
    public List<StaticLevelObject> assets;
    public int assetsCount => assets.Count;

    public ISynchronizedObject Create(int assetID)
    {
        var clone = Instantiate(assets[assetID], Vector3.zero, Quaternion.identity).GetComponent<ISynchronizedObject>();
        return clone;
    }
}
