namespace Gu.Xml
{
    using System;

    internal static class TypeExt
    {
        internal static bool IsAnonymous(this Type type) => type.Name.StartsWith("<>f__AnonymousType");

        internal static bool IsNullable(this Type type) => type.FullName.StartsWith("System.Nullable`1");
    }
}
