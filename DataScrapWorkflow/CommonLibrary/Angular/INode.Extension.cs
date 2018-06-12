using AngleSharp.Dom;

namespace CommonLibrary
{
    /// <summary>
    /// An extension class helper for <see cref="INode"/> interfaces
    /// </summary>
    public static class INode_Extension
    {
        /// <summary>
        /// Checks if the node name is equal to the node name of <see cref="INode"/>
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool IsEqual(this INode element, string name)
        {
            if (element == null) return false;
            return string.Compare(element.NodeName, name, true) == 0;
        }

        /// <summary>
        /// Checks if the node name is equal to the node name of <see cref="INode"/>
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool IsEqualCase(this INode element, string name)
        {
            if (element == null) return false;
            return string.Compare(element.NodeName, name, false) == 0;
        }
    }
}
