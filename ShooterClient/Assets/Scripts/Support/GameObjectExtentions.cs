using UnityEngine;

public static class GameObjectExtentions
{
    public static bool HasComponent<T>(this GameObject gameObject) where T : Component
    {
        bool result = false;
        if (gameObject.GetComponent<T>()) result = true;
        return result;
    }
}
