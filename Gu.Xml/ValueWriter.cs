namespace Gu.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    public abstract class ValueWriter
    {
        private static readonly Dictionary<Type, ValueWriter> SimpleValueWriters = CreateWriters(
            Create<bool>((writer, value) => writer.Write(value ? "true" : "false")),
            Create<int>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo))),
            Create<double>(WriteDouble));

        public static bool TryGetSimple<T>(T value, out ValueWriter writer)
        {
            return SimpleValueWriters.TryGetValue(value.GetType(), out writer);
        }

        public abstract void Write<T>(TextWriter writer, T value);

        private static Dictionary<Type, ValueWriter> CreateWriters(params (Type, ValueWriter)[] writers)
        {
            return writers.ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);
        }

        private static (Type, ValueWriter) Create<T>(Action<TextWriter, T> write)
        {
            return (typeof(T), new ValueWriter<T>(write));
        }

        private static void WriteDouble(TextWriter writer, double value)
        {
            // https://www.w3.org/TR/xmlschema-2/#double
            if (double.IsNegativeInfinity(value))
            {
                writer.Write("-INF");
                return;
            }

            if (double.IsPositiveInfinity(value))
            {
                writer.Write("INF");
                return;
            }

            if (value == 0 && BitConverter.DoubleToInt64Bits(value) != BitConverter.DoubleToInt64Bits(0.0))
            {
                writer.Write("-0");
                return;
            }

            writer.Write(value.ToString("R", NumberFormatInfo.InvariantInfo));
        }
    }
}