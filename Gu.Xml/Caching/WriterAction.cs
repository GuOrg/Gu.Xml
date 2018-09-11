namespace Gu.Xml
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Reflection;

    public static class WriterAction
    {
        private static readonly CastActions<TextWriter> TextWriterActions = new CastActions<TextWriter>();

        static WriterAction()
        {
            TextWriterActions.RegisterClass<string>((writer, value) => writer.Write(value));
            TextWriterActions.RegisterStruct<bool>((writer, value) => writer.Write(value ? "true" : "false"));
            TextWriterActions.RegisterStruct<byte>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)));
            TextWriterActions.RegisterStruct<char>((writer, value) => writer.Write((int)value));
            TextWriterActions.RegisterStruct<decimal>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)));
            TextWriterActions.RegisterStruct<double>((writer, value) => writer.Write(ToString(value)));
            TextWriterActions.RegisterStruct<float>((writer, value) => writer.Write(ToString(value)));
            TextWriterActions.RegisterStruct<Guid>((writer, value) => writer.Write(value.ToString()));
            TextWriterActions.RegisterStruct<int>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)));
            TextWriterActions.RegisterStruct<long>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)));
            TextWriterActions.RegisterStruct<short>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)));
            TextWriterActions.RegisterStruct<sbyte>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)));
            TextWriterActions.RegisterStruct<ulong>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)));
            TextWriterActions.RegisterStruct<uint>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)));
            TextWriterActions.RegisterStruct<ushort>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)));
        }

        public static bool TryGetSimple<TMember>(TMember value, out Action<TextWriter, TMember> writer)
        {
            return WriterActionCache<TMember>.TryGetSimple(value, out writer);
        }

        /// <summary>
        /// Try getting a writer for writing the element contents as a string.
        /// </summary>
        /// <typeparam name="TMember">
        /// The type of the member.
        /// If an int is stored in a property of type object <typeparamref name="TMember"/> will be <see cref="object"/> and <paramref name="type"/> will be <see cref="int"/>
        /// </typeparam>
        /// <param name="type">The type of the value.</param>
        /// <param name="writer"></param>
        /// <returns>True if a writer was found for <paramref name="type"/></returns>
        private static bool TryGetSimple<TMember>(Type type, out Action<TextWriter, TMember> writer)
        {
            if (TextWriterActions.TryGet(type, out var castAction))
            {
                if (castAction.TryGet<TMember>(out writer))
                {
                    return true;
                }

                return TryGetSimple(typeof(TMember), out writer);
            }

            if (type.IsEnum)
            {
                // ReSharper disable once PossibleNullReferenceException
                _ = typeof(CastActionsExt).GetMethod(nameof(CastActionsExt.RegisterEnum), BindingFlags.Public | BindingFlags.Static)
                                             .MakeGenericMethod(type)
                                             .Invoke(null, new[] { TextWriterActions });
                return TryGetSimple(typeof(TMember), out writer);
            }

            writer = null;
            return false;
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

            // ReSharper disable once CompareOfFloatsByEqualityOperator
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

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (value == 0 &&
                BitConverter.DoubleToInt64Bits(value) != BitConverter.DoubleToInt64Bits(0.0))
            {
                return "-0";
            }

            return value.ToString("R", NumberFormatInfo.InvariantInfo);
        }

        private static class WriterActionCache<T>
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
                if (TryGetSimple<T>(type, out var defaultAction))
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
}