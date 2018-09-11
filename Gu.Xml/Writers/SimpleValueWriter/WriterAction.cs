namespace Gu.Xml
{
    using System;
    using System.IO;

    public class WriterAction<T>
    {
        // ReSharper disable once StaticMemberInGenericType
        private static bool isValueCreated;
        private static Action<TextWriter, T> action;

        public static bool TryGetSimple(T value, out Action<TextWriter, T> write)
        {
            if (isValueCreated)
            {
                write = action;
                return write != null;
            }

            var type = value.GetType();
            if (DefaultTextWriterActions.Default.TryGetValue(type, out var defaultAction))
            {
                if (typeof(T) == type)
                {
                    action = write = (Action<TextWriter, T>)defaultAction;
                    isValueCreated = true;
                    return true;
                }

                write = (writer, writeValue) => ((Delegate)defaultAction).DynamicInvoke(writer, writeValue);
                return true;
            }

            if (type.IsEnum)
            {
                action = write = (Action<TextWriter, T>)DefaultTextWriterActions.Default.GetOrAdd(type, t => new Action<TextWriter, T>((writer, writeValue) => writer.Write(Enum.Format(t, writeValue, "G").Replace(",", string.Empty))));
                return true;
            }

            write = null;
            return false;
        }
    }
}