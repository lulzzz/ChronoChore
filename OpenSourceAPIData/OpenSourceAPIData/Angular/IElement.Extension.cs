using System;
using AngleSharp.Dom;

namespace WebScrap.LibExtension
{
    /// <summary>
    /// An extension class for <see cref="IElement"/>
    /// </summary>
    public static class IElement_Extension
    {
        /// <summary>
        /// Checks if the value for the attribute is null or whitespace. 
        /// </summary>
        /// <param name="element"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static bool IsValueEmpty(this IElement element, string attributeName)
        {
            return element.HasAttribute(attributeName) &&
                string.IsNullOrWhiteSpace(element.GetAttribute(attributeName));
        }

        /// <summary>
        /// It casts the value returned by <see cref="IElement.GetAttribute(string)"/> to int.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attributeName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetAttributeInt(this IElement element, string attributeName,
            int defaultValue = 0)
        {
            string value = element.GetAttribute(attributeName);

            if (string.IsNullOrWhiteSpace(value)) return defaultValue;
            else return Convert.ToInt32(value);
        }

        /// <summary>
        /// Check if the element node name starts with the string
        /// </summary>
        /// <param name="element"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool NameStartsWith(this IElement element, string startString,
            bool ignoreCase = false)
        {
            if(ignoreCase)
                return startString.StartsWith(element.NodeName, 
                    StringComparison.InvariantCultureIgnoreCase);
            else
                return startString.StartsWith(element.NodeName);
        }
    }
}
