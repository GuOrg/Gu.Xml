namespace Gu.Xml
{
    using System;
    using System.Globalization;

    internal static class XmlFormat
    {
        /// <summary>
        /// Format a <see cref="double"/> according to: https://www.w3.org/TR/xmlschema-2/#double
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The string representation.</returns>
        internal static string ToString(double value)
        {
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

        /// <summary>
        /// Format a <see cref="float"/> according to: https://www.w3.org/TR/xmlschema-2/#double
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The string representation.</returns>
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
