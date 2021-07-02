using UnityEngine;

public static class StringExtensions
{
    public static string WithColour(this string text, Color colour)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGB(colour)}>{text}</color>";
    }
}
