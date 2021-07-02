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

    public static Vector3 RandomVector3(float dispection)
    {
        return new Vector3(Random.Range(-dispection, dispection), Random.Range(-dispection, dispection), Random.Range(-dispection, dispection));
    }
}