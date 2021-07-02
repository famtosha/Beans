using System;

public static class ArrayExtentions
{
    public static bool GetEmptyIndex<T>(this T[] array, out int index) where T : class
    {
        index = 0;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == null)
            {
                index = i;
                return true;
            }
        }
        return false;
    }

    public static void ForeachExpect<T>(this T[] array, Action<T> action, int expect)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != null && i != expect) action(array[i]);
        }
    }

    public static void Foreach<T>(this T[] array, Action<T> action)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != null) action(array[i]);
        }
    }
}
