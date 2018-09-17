namespace Gu.Xml
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Helper methods for <see cref="MemberInfo"/>.
    /// </summary>
    internal static class MemberInfoExt
    {
        /// <summary>
        /// Calls (T)Attribute.GetCustomAttribute(member, typeof(T));.
        /// </summary>
        /// <typeparam name="T">The attribute type.</typeparam>
        /// <param name="member">The <see cref="MemberInfo"/>.</param>
        /// <param name="attribute">The <see cref="T"/>.</param>
        /// <returns>True if an attribute was found.</returns>
        internal static bool TryGetCustomAttribute<T>(this MemberInfo member, out T attribute)
            where T : Attribute
        {
            attribute = member.GetCustomAttribute<T>();
            return attribute != null;
        }
    }
}