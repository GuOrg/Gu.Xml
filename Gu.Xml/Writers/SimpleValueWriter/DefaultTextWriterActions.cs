namespace Gu.Xml
{
    using System;
    using System.Collections.Concurrent;
    using System.Globalization;
    using System.IO;

    public static class DefaultTextWriterActions
    {
        internal static readonly ConcurrentDictionary<Type, object> Default = new ConcurrentDictionary<Type, object>();

        static DefaultTextWriterActions()
        {
            Default.TryAdd(typeof(string), new Action<TextWriter, string>((writer, value) => writer.Write(value)));

            Add<bool>((writer, value) => writer.Write(value ? "true" : "false"));
            Add<byte>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)));
            Add<char>((writer, value) => writer.Write((int)value));
            Add<decimal>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)));
            Add<double>((writer, value) => writer.Write(ToString(value)));
            Add<float>((writer, value) => writer.Write(ToString(value)));
            Add<Guid>((writer, value) => writer.Write(value.ToString()));
            Add<int>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)));
            Add<long>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)));
            Add<short>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)));
            Add<sbyte>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)));
            Add<ulong>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)));
            Add<uint>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)));
            Add<ushort>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)));

            void Add<T>(Action<TextWriter, T> write)
                where T : struct
            {
                Default.TryAdd(typeof(T), write);
                Default.TryAdd(typeof(T?), write);
            }
        }

        private static string ToString(double value)
        {
            // https://www.w3.org/TR/xmlschema-2/#double
            if (double.IsNegativeInfinity(value))
            {
                return "-INF";
            }

            if (double.IsPositiveInfinity(value))
            {
                return "INF";
            }

            if (value == 0 &&
                BitConverter.DoubleToInt64Bits(value) != BitConverter.DoubleToInt64Bits(0.0))
            {
                return "-0";
            }

            return value.ToString("R", NumberFormatInfo.InvariantInfo);
        }

        private static string ToString(float value)
        {
            // https://www.w3.org/TR/xmlschema-2/#double
            if (float.IsNegativeInfinity(value))
            {
                return "-INF";
            }

            if (float.IsPositiveInfinity(value))
            {
                return "INF";
            }

            if (value == 0 &&
                BitConverter.DoubleToInt64Bits(value) != BitConverter.DoubleToInt64Bits(0.0))
            {
                return "-0";
            }

            return value.ToString("R", NumberFormatInfo.InvariantInfo);
        }
    }
}