namespace Gu.Xml
{
    using System;
    using System.Reflection;

    internal static class MemberInfoExt
    {
        internal static bool TryGetCustomAttribute<T>(this MemberInfo member, out T attribute)
            where T : Attribute
        {
            attribute = (T)Attribute.GetCustomAttribute(member, typeof(T));
            return attribute != null;
        }
    }
}