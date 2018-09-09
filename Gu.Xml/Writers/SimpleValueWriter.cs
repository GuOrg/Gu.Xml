namespace Gu.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;

    public abstract class SimpleValueWriter
    {
        private static readonly Dictionary<Type, SimpleValueWriter> Default = new Dictionary<Type, SimpleValueWriter>();

        static SimpleValueWriter()
        {
            Add<bool>((writer, value) => writer.Write(value ? "true" : "false"));
            Add<int>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)));
            Add<double>((writer, value) => writer.Write(ToString(value)));

            void Add<T>(Action<TextWriter, T> write)
                where T : struct
            {
                var valueWriter = new SimpleValueWriter<T>(write);
                Default.Add(typeof(T), valueWriter);
                Default.Add(typeof(T?), valueWriter);
            }
        }

        public static bool TryGet<T>(T value, out SimpleValueWriter writer)
        {
            return Default.TryGetValue(value.GetType(), out writer);
        }

        public abstract void Write<T>(TextWriter writer, T value);

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
    }
}