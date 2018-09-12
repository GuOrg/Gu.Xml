namespace Gu.Xml
{
    using System;
    using System.Globalization;

    internal static class XmlFormat
    {
        internal static string ToString(double value)
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

        internal static string ToString(float value)
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
    }
}
