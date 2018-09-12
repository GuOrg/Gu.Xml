namespace Gu.Xml
{
    internal static class StringExt
    {
        internal static bool TryLastIndexOf(this string text, char c, out int index)
        {
            index = text.LastIndexOf(c);
            return index > -1;
        }
    }
}