namespace Gu.Xml
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Reflection;

    public static class WriterAction
    {
        private static readonly TextWriterActions TextWriterActions = new TextWriterActions();

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
        public static bool TryGetSimple<TMember>(Type type, out Action<TextWriter, TMember> writer)
        {
            if (TextWriterActions.TryGet(type, out var castAction))
            {
                if (type == typeof(TMember))
                {
                    writer = castAction.Raw<TMember>();
                    return true;
                }

                if (castAction.Boxed() is Action<TextWriter, TMember> match)
                {
                    writer = match;
                    return true;
                }

                return TryGetSimple(typeof(TMember), out writer);
            }

            if (type.IsEnum)
            {
                _ = typeof(TextWriterActions).GetMethod(nameof(Gu.Xml.TextWriterActions.RegisterEnum), BindingFlags.Public | BindingFlags.Instance)
                                             .MakeGenericMethod(type)
                                             .Invoke(TextWriterActions, null);
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