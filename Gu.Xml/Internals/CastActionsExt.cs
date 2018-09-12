namespace Gu.Xml
{
    using System;
    using System.IO;

    /// <summary>
    /// A cache of <see cref="CastAction{TextWriter}"/>
    /// </summary>
    internal static class CastActionsExt
    {
        internal static void RegisterEnum<T>(this CastActions<TextWriter> actions)
            where T : struct, Enum
        {
            actions.RegisterStruct<T>((writer, value) => EnumWriter<T>.Default.Write(writer, value));
        }
    }
}