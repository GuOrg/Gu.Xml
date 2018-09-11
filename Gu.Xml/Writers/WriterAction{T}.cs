namespace Gu.Xml
{
    using System;
    using System.IO;

    public class WriterAction<T>
    {
        // ReSharper disable once StaticMemberInGenericType
        private static bool isValueCreated;
        private static Action<TextWriter, T> simple;

        public static bool TryGetSimple(T value, out Action<TextWriter, T> write)
        {
            if (isValueCreated)
            {
                write = simple;
                return write != null;
            }

            var type = value.GetType();
            if (WriterAction.TryGetSimple<T>(type, out var defaultAction))
            {
                if (typeof(T) == type)
                {
                    simple = write = defaultAction;
                    isValueCreated = true;
                    return true;
                }

                write = defaultAction;
                return true;
            }

            write = null;
            return false;
        }
    }
}