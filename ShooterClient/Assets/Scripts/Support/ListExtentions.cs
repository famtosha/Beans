using UnityEngine;
using System.Collections.Generic;

public static class ListExtentions
{
    public static T GetRandom<T>(this IList<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}
