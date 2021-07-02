using UnityEngine;

public interface ISynchronizedObjectsFacotry
{
    ISynchronizedObject Create(int assetID, Vector3 position, Quaternion rotation, Vector3 scale);
}
