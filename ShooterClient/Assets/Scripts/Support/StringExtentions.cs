public static class StringExtentions
{
    public static string RemoveTMPChars(this string str)
    {
        return str.Trim((char)8203);
    }
}
