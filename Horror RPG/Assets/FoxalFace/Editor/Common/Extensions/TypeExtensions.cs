namespace FoxalFace.Common.Editor
{
    using System;
    using System.Collections.Generic;


    public static class TypeExtensions
    {
        #region Attributes
        private static Dictionary<Type, bool> visibilityCache = new Dictionary<Type, bool>();
        #endregion

        #region API
        public static bool IsVisibleInInspector(this Type type)
        {
            if (visibilityCache.TryGetValue(type, out bool result))
                return result;

            result =
                typeof(UnityEngine.Object).IsAssignableFrom(type)
                || ((type.IsValueType || type.IsClass)
                    && type.IsDefined(typeof(SerializableAttribute), false)
                    && !type.IsGenericType);

            visibilityCache[type] = result;
            return result;
        }

        public static bool IsSerializable(this Type type)
        {
            if (typeof(UnityEngine.Object).IsAssignableFrom(type))
                return true;

            if (type.IsPrimitive || type == typeof(string))
                return true;

            if (type.IsArray)
                return IsSerializable(type.GetElementType());

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                return IsSerializable(type.GetGenericArguments()[0]);

            if (type.IsSerializable && !type.IsGenericType)
                return true;

            return false;
        }
        #endregion
    }
}
