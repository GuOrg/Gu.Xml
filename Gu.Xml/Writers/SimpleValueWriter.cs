﻿namespace Gu.Xml
{
    using System;
    using System.Collections.Concurrent;
    using System.Globalization;
    using System.IO;

    public abstract class SimpleValueWriter
    {
        private static readonly ConcurrentDictionary<Type, SimpleValueWriter> Default = new ConcurrentDictionary<Type, SimpleValueWriter>();

        static SimpleValueWriter()
        {
            Default.TryAdd(typeof(string), new SimpleValueWriter<string>((writer, value) => writer.Write(value)));

            Add<bool>((writer, value) => writer.Write(value ? "true" : "false"));
            Add<char>((writer, value) => writer.Write((int)value));
            Add<int>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)));
            Add<double>((writer, value) => writer.Write(ToString(value)));

            void Add<T>(Action<TextWriter, T> write)
                where T : struct
            {
                var valueWriter = new SimpleValueWriter<T>(write);
                Default.TryAdd(typeof(T), valueWriter);
                Default.TryAdd(typeof(T?), valueWriter);
            }
        }

        public static bool TryGet<T>(T value, out SimpleValueWriter writer)
        {
            if (value == null)
            {
                writer = null;
                return false;
            }

            if (typeof(T).IsEnum)
            {
                writer = Default.GetOrAdd(typeof(T), EnumWriter.Default);
            }

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