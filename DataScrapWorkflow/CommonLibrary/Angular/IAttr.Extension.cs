using System;
using AngleSharp.Dom;

namespace CommonLibrary
{
    /// <summary>
    /// Extension class for <see cref="IAttr"/>
    /// </summary>
    public static class IAttr_Extension
    {
        /// <summary>
        /// Checks if the node name is equal to the node name of <see cref="INode"/>
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool IsNameEqual(this IAttr attr, string name) => (attr == null) ? false :
                string.Compare(attr.Name, name, true) == 0;

        /// <summary>
        /// Checks if the node name is equal to the node name of <see cref="INode"/>
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool IsNameEqualCase(this IAttr attr, string name) => (attr == null) ? false :
                string.Compare(attr.Name, name, false) == 0;

        /// <summary>
        /// Checks a boolean flag and return true if the value is  yes / ok
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        public static bool IsValueTruth(this IAttr attr)
        {
            var value = attr.Value.ToUpper();
            return value == "OK" || value == "YES";
        }

        /// <summary>
        /// Checks if the attribute name starts with a string
        /// </summary>
        /// <param name="attr"></param>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static bool NameStartsWith(this IAttr attr, string startString, bool ignoreCase = false)
        {
            if (ignoreCase)
                return attr.Name.StartsWith(startString, StringComparison.InvariantCultureIgnoreCase);
            else
                return attr.Name.StartsWith(startString);
        }

        /// <summary>
        /// Checks if the value for the attribute is null or whitespace. 
        /// </summary>
        /// <param name="element"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static bool IsValueEmpty(this IAttr attr) => string.IsNullOrWhiteSpace(attr.Value);

        /// <summary>
        /// Check if the value is a integer
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        public static bool IsValueInt(this IAttr attr)
        {
            int value = 0;
            bool isInt = int.TryParse(attr.Value, out value);
            return isInt;
        }
    }
}
