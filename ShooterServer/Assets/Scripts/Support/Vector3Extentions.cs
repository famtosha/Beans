using UnityEngine;

public static class Vector3Extentions
{
    public static Vector2 ToVector2(this Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.y);
    }

    public static Vector3 SetX(this Vector3 vector, float newX)
    {
        return new Vector3(newX, vector.y, vector.z);
    }

    public static Vector3 SetY(this Vector3 vector, float newY)
    {
        return new Vector3(vector.x, newY, vector.z);
    }

    public static Vector3 SetZ(this Vector3 vector, float newZ)
    {
        return new Vector3(vector.x, vector.y, newZ);
    }
}
