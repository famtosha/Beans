using UnityEngine;

public interface ISynchronizedObjectsFacotry
{
    ISynchronizedObject Create(int assetID);
}
