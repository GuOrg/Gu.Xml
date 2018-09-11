namespace Gu.Xml
{
    using System;
    using System.IO;

    public class SimpleActions : CastActions<TextWriter>
    {
        public void RegisterEnum<T>()
            where T : struct, Enum
        {
            this.Cache[typeof(T)] = CastAction<TextWriter>.Create(new Action<TextWriter, T>((writer, value) => EnumWriter<T>.Default.Write(writer, value)));
            if (!this.Cache.ContainsKey(typeof(T?)))
            {
                this.Cache[typeof(T?)] = CastAction<TextWriter>.Create(new Action<TextWriter, T?>((writer, value) => EnumWriter<T>.Default.Write(writer, value.Value)));
            }
        }
    }
}