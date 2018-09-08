namespace Gu.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;

    public abstract class ValueWriter
    {
        private static readonly Dictionary<Type, ValueWriter> SimpleValueWriters = new Dictionary<Type, ValueWriter>();

        static ValueWriter()
        {
            Add<bool>((writer, value) => writer.Write(value ? "true" : "false"));
            Add<int>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)));
            Add<double>(WriteDouble);

            void Add<T>(Action<TextWriter, T> write)
                where T : struct
            {
                var valueWriter = new ValueWriter<T>(write);
                SimpleValueWriters.Add(typeof(T), valueWriter);
                SimpleValueWriters.Add(typeof(T?), valueWriter);
            }
        }

        public static bool TryGetSimple<T>(T value, out ValueWriter writer)
        {
            return SimpleValueWriters.TryGetValue(value.GetType(), out writer);
        }

        public abstract void Write<T>(TextWriter writer, T value);

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