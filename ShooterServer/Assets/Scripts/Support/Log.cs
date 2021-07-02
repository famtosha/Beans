using System;
using UnityEngine;

public static class Log
{
    public static void Write(string message)
    {
#if UNITY_EDITOR
        Debug.Log(message);
#else
        Debug.Log(AddTime($"[INFO]: {message}"));
#endif

    }

    public static void WriteWarning(string message)
    {
#if UNITY_EDITOR
        Debug.LogWarning(message);
#else
        Console.ForegroundColor = ConsoleColor.Yellow;
        Debug.Log(AddTime($"[Warning]: {message}"));
#endif

    }

    public static void WriteError(string message)
    {
#if UNITY_EDITOR
        Debug.LogError(message);
#else
        Console.ForegroundColor = ConsoleColor.Red;
        Debug.Log(AddTime($"[ERROR]: {message}"));
#endif
    }

    private static string AddTime(string message)
    {
        return $"[{DateTime.Now.TimeOfDay}] {message}";
    }
}
