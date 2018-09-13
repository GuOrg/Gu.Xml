namespace Gu.Xml
{
    using System;
    using System.Collections.Generic;

    internal static class TypeExt
    {
        internal static bool IsAnonymous(this Type type) => type.Name.StartsWith("<>f__AnonymousType");

        internal static bool IsNullable(this Type type) => type.FullName?.StartsWith("System.Nullable`1") == true;

        internal static bool IsGenericEnumerable(this Type type, out Type enumerable)
        {
            return type.GetInterfaces().TrySingle(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>), out enumerable);
        }
    }
}
