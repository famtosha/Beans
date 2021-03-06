using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtentions
{
    public static bool HasComponent<T>(this GameObject gameObject)
    {
        bool result = false;
        if (gameObject.GetComponent<T>() != null) result = true;
        return result;
    }

    public static List<Transform> GetAllChilds(this Transform transform)
    {
        List<Transform> result = new List<Transform>();
        foreach (Transform t in transform)
        {
            result.Add(t);
            result.AddRange(GetAllChilds(t));
        }
        return result;
    }
}
