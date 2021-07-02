using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "SOLF", menuName = "Factories/SOLF")]
public class SynchronizedObjectsFacotry : ScriptableObject, ISynchronizedObjectsFacotry
{
    public List<GameObject> assets;

    public ISynchronizedObject Create(int assetID, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        var obj = Object.Instantiate(assets[assetID], position, rotation).GetComponent<ISynchronizedObject>();
        obj.assetID = assetID;
        return obj;
    }
}