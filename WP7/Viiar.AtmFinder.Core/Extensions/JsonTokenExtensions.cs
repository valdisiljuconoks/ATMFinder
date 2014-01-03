using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace Viiar.AtmFinder.Core.Extensions
{
    public static class JsonTokenExtensions
    {
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static string SafeValue(this JToken token, string defaultValue)
        {
            try
            {
                if (token == null)
                {
                    return defaultValue;
                }

                return token.ToString();
            }
            catch(Exception)
            {
                return defaultValue;
            }
        }

        public static double SafeValue(this JToken value, double defaultValue, string decimalSeperator = ".")
        {
            var temp = value.SafeValue(string.Empty);
            if (string.IsNullOrEmpty(temp))
            {
                return defaultValue;
            }

            temp = temp.Replace(",", decimalSeperator);
            temp = temp.Trim();
            return Convert.ToDouble(temp, CultureInfo.InvariantCulture);
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static int SafeValue(this JToken value, int defaultValue)
        {
            try
            {
                if (value == null)
                {
                    return defaultValue;
                }

                switch (value.Type)
                {
                    case JTokenType.String:
                        int result;
                        if (int.TryParse((string)value, out result))
                        {
                            return result;
                        }

                        break;
                    case JTokenType.Integer:
                        return (int)value;
                    case JTokenType.Float:
                        return (int)(float)value;
                    default:
                        return defaultValue;
                }

                return defaultValue;
            }
            catch(Exception)
            {
                return defaultValue;
            }
        }
    }
}
