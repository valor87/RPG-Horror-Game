namespace FoxalFace.Common.Editor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEngine;


    public static partial class FieldInfoExtensions
    {
        #region API
        public static bool IsVisibleInInspector(this FieldInfo fieldInfo)
        {
            if (fieldInfo.IsNotSerialized)
                return false;

            bool hasSerializeField = fieldInfo.IsDefined(typeof(SerializeField), inherit: true);
            if (!fieldInfo.IsPublic && !hasSerializeField)
                return false;

            Type fieldType = fieldInfo.FieldType;

            if (!fieldType.IsSerializable())
                return false;

            return true;
        }

        public static Type GetReferencedType(this FieldInfo fieldInfo)
        {
            Type type = fieldInfo.FieldType;

            if (type.IsArray)
                return type.GetElementType();

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                return type.GenericTypeArguments[0];

            return type;
        }

        public static bool IsArray(this FieldInfo fieldInfo)
        {
            return fieldInfo.FieldType.IsArray ||
                (fieldInfo.FieldType.IsGenericType && fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(List<>));
        }

        public static IEnumerable<FieldInfo> DistinctBySignature(this IEnumerable<FieldInfo> fields)
        {
            return fields.Distinct(FieldInfoComparer.instance);
        }
        #endregion
    }


    public class FieldInfoComparer : IEqualityComparer<FieldInfo>
    {
        public static readonly FieldInfoComparer instance = new FieldInfoComparer();

        public bool Equals(FieldInfo x, FieldInfo y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null || y is null) return false;

            return x.Name == y.Name &&
                   x.DeclaringType == y.DeclaringType &&
                   x.FieldType == y.FieldType &&
                   x.IsPublic == y.IsPublic &&
                   x.IsPrivate == y.IsPrivate &&
                   x.IsFamily == y.IsFamily;
        }

        public int GetHashCode(FieldInfo obj)
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + obj.Name.GetHashCode();
                hash = hash * 23 + (obj.DeclaringType?.GetHashCode() ?? 0);
                hash = hash * 23 + (obj.FieldType?.GetHashCode() ?? 0);
                hash = hash * 23 + obj.IsPublic.GetHashCode();
                hash = hash * 23 + obj.IsPrivate.GetHashCode();
                hash = hash * 23 + obj.IsFamily.GetHashCode();
                return hash;
            }
        }
    }
}
