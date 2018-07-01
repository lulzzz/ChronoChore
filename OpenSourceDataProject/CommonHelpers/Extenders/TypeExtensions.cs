using System;

namespace CommonHelpers.Extenders
{
    public static class TypeExtensions
    {
        public static T GetCustomAttribute<T>(this Type type) where T : Attribute
        {
            var attributes = type.GetCustomAttributes(false);

            if (attributes != null && attributes.Length > 0)
            {
                Type attributeType = typeof(T);
                foreach (Attribute attrib in attributes)
                {
                    if (attributeType == attrib.GetType())
                        return (T)attrib;
                }
            }

            return null;
        }
    }
}
