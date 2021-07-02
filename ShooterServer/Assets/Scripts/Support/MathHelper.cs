using UnityEngine;

public static class MathHelper
{
    public static bool RandomBool(float change)
    {
        return UnityEngine.Random.Range(0, 1f) < change;
    }

    public static bool RandomBool()
    {
        return RandomBool(0.5f);
    }

    public static Vector3 RandomVector3(float range)
    {
        return new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range));
    }

    public static Vector2 RandomVector2(float range)
    {
        return new Vector3(Random.Range(-range, range), Random.Range(-range, range));
    }
}