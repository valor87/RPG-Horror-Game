namespace FoxalFace.Common.Editor
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;


    public static partial class SerializedPropertyExtensions
    {
        #region Attributes
        private static MethodInfo getFieldInfoFromPropertyInfo;

        private static MethodInfo getHandlerInfo;

        private static FieldInfo reorderableListsInfo;

        private static Type listWrapperType;

        private static MethodInfo getPropertyIdentifierInfo;

        private static FieldInfo reorderableListInfo;
        #endregion

        #region API
        public static int GetGUID(this SerializedProperty property)
        {
            if (property == null || property.serializedObject == null)
                return -1;

            if (string.IsNullOrWhiteSpace(property.propertyPath) || !property.serializedObject.targetObject)
                return -1;

            return EditorHelper.GetPropertyGUID(property.serializedObject.targetObject, property.propertyPath);
        }

        public static bool IsArrayElement(this SerializedProperty property)
        {
            return Regex.IsMatch(property.propertyPath, DrawerHelper.kArrayDataPattern + "$");
        }

        public static FieldInfo GetFieldInfo(this SerializedProperty property, out Type type)
        {
            return GetFieldInfoFromProperty(property, out type);
        }

        public static bool TryGetCustomAttribute<T>(this SerializedProperty property, out T attribute) where T : PropertyAttribute
        {
            attribute = null;

            FieldInfo fieldInfo = GetFieldInfoFromProperty(property, out Type _);
            if (fieldInfo == null)
                return false;

            attribute = fieldInfo.GetCustomAttribute<T>();

            return attribute != null;
        }

        public static Type GetFieldType(this SerializedProperty property)
        {
            GetFieldInfoFromProperty(property, out Type type);

            return type;
        }

        public static object GetHandler(this SerializedProperty property)
        {
            try
            {
                if (getHandlerInfo == null)
                {
                    getHandlerInfo = Type.GetType("UnityEditor.ScriptAttributeUtility, UnityEditor")
                        .GetMethod("GetHandler", BindingFlags.Static | BindingFlags.NonPublic);
                }

                return getHandlerInfo.Invoke(null, new object[] { property });
            }
            catch (Exception)
            {
                throw new Exception("The method named 'GetHandler' in UnityEditor.ScriptAttributeUtility not found!");
            }
        }

        public static object GetValue(this SerializedProperty property)
        {
            return property.propertyType switch
            {
                SerializedPropertyType.Integer =>
                    property.intValue,
                SerializedPropertyType.Boolean =>
                    property.boolValue,
                SerializedPropertyType.Float =>
                    property.floatValue,
                SerializedPropertyType.String =>
                    property.stringValue,
                SerializedPropertyType.Color =>
                    property.colorValue,
                SerializedPropertyType.ObjectReference =>
                    property.objectReferenceValue,
                SerializedPropertyType.LayerMask =>
                    property.intValue,
                SerializedPropertyType.Enum =>
                    property.enumValueIndex,
                SerializedPropertyType.Vector2 =>
                    property.vector2Value,
                SerializedPropertyType.Vector3 =>
                    property.vector3Value,
                SerializedPropertyType.Vector4 =>
                    property.vector4Value,
                SerializedPropertyType.Rect =>
                    property.rectValue,
                SerializedPropertyType.ArraySize =>
                    property.arraySize,
                SerializedPropertyType.Character =>
                    property.intValue,
                SerializedPropertyType.AnimationCurve =>
                    property.animationCurveValue,
                SerializedPropertyType.Bounds =>
                    property.boundsValue,
                SerializedPropertyType.Gradient =>
                    GetGradientValue(property),
                SerializedPropertyType.Quaternion =>
                    property.quaternionValue,
                SerializedPropertyType.ExposedReference =>
                    property.exposedReferenceValue,
                SerializedPropertyType.FixedBufferSize =>
                    property.fixedBufferSize,
                SerializedPropertyType.Vector2Int =>
                    property.vector2IntValue,
                SerializedPropertyType.Vector3Int =>
                    property.vector3IntValue,
                SerializedPropertyType.RectInt =>
                    property.rectIntValue,
                SerializedPropertyType.BoundsInt =>
                    property.boundsIntValue,
                SerializedPropertyType.ManagedReference =>
                    property.managedReferenceValue,
                SerializedPropertyType.Hash128 =>
                    property.hash128Value,
                SerializedPropertyType.Generic =>
                    GetGenericValue(property),
                _ => null
            };
        }

        public static void SetValue(this SerializedProperty property, object value)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    property.intValue = (int)value;
                    break;
                case SerializedPropertyType.Boolean:
                    property.boolValue = (bool)value;
                    break;
                case SerializedPropertyType.Float:
                    property.floatValue = (float)value;
                    break;
                case SerializedPropertyType.String:
                    property.stringValue = (string)value;
                    break;
                case SerializedPropertyType.Color:
                    property.colorValue = (Color)value;
                    break;
                case SerializedPropertyType.ObjectReference:
                    property.objectReferenceValue = (UnityEngine.Object)value;
                    break;
                case SerializedPropertyType.LayerMask:
                    property.intValue = (int)value;
                    break;
                case SerializedPropertyType.Enum:
                    property.enumValueIndex = (int)value;
                    break;
                case SerializedPropertyType.Vector2:
                    property.vector2Value = (Vector2)value;
                    break;
                case SerializedPropertyType.Vector3:
                    property.vector3Value = (Vector3)value;
                    break;
                case SerializedPropertyType.Vector4:
                    property.vector4Value = (Vector4)value;
                    break;
                case SerializedPropertyType.Rect:
                    property.rectValue = (Rect)value;
                    break;
                case SerializedPropertyType.ArraySize:
                    property.arraySize = (int)value;
                    break;
                case SerializedPropertyType.Character:
                    property.intValue = (int)value;
                    break;
                case SerializedPropertyType.AnimationCurve:
                    property.animationCurveValue = (AnimationCurve)value;
                    break;
                case SerializedPropertyType.Bounds:
                    property.boundsValue = (Bounds)value;
                    break;
                case SerializedPropertyType.Gradient:
                    property.SetGradientValue((Gradient)value);
                    break;
                case SerializedPropertyType.Quaternion:
                    property.quaternionValue = (Quaternion)value;
                    break;
                case SerializedPropertyType.ExposedReference:
                    property.exposedReferenceValue = (UnityEngine.Object)value;
                    break;
                case SerializedPropertyType.FixedBufferSize:
                    // Not settable
                    break;
                case SerializedPropertyType.Vector2Int:
                    property.vector2IntValue = (Vector2Int)value;
                    break;
                case SerializedPropertyType.Vector3Int:
                    property.vector3IntValue = (Vector3Int)value;
                    break;
                case SerializedPropertyType.RectInt:
                    property.rectIntValue = (RectInt)value;
                    break;
                case SerializedPropertyType.BoundsInt:
                    property.boundsIntValue = (BoundsInt)value;
                    break;
                case SerializedPropertyType.ManagedReference:
                    property.managedReferenceValue = value;
                    break;
                case SerializedPropertyType.Hash128:
                    property.hash128Value = (Hash128)value;
                    break;
                case SerializedPropertyType.Generic:
                    property.SetGenericValue(value);
                    break;
            }
        }

        public static bool HasDefaultValue(this SerializedProperty property, Type type = null)
        {
            if (type == null)
            {
                FieldInfo fieldInfo = GetFieldInfoFromProperty(property, out type);

                if (fieldInfo == null)
                    return false;
            }

            return IsEqualDefault(property.GetValue(), type);
        }

        public static ReorderableList GetReorderableList(this SerializedProperty property, out object listWrapper)
        {
            listWrapper = null;

            if (!property.isArray)
                return null;

            // TODO: optimization with cache

            IDictionary reorderableLists = ReorderableLists;

            string key = GetPropertyIdentifier(property);

            if (!reorderableLists.Contains(key))
                return null;

            listWrapper = reorderableLists[key];

            Type listWrapperType = ListWrapperType;

            try
            {
                if (reorderableListInfo == null)
                {
                    reorderableListInfo = listWrapperType
                        .GetField("m_ReorderableList", BindingFlags.Instance | BindingFlags.NonPublic);
                }

                return (ReorderableList)reorderableListInfo.GetValue(listWrapper);
            }
            catch (Exception)
            {
                throw new Exception("Failed to get value to the 'm_ReorderableList' field in " + listWrapperType + "!");
            }
        }
        #endregion

        #region Private
        private static FieldInfo GetFieldInfoFromProperty(SerializedProperty property, out Type type)
        {
            try
            {
                if (getFieldInfoFromPropertyInfo == null)
                {
                    getFieldInfoFromPropertyInfo = Type.GetType("UnityEditor.ScriptAttributeUtility, UnityEditor")
                        .GetMethod("GetFieldInfoFromProperty", BindingFlags.NonPublic | BindingFlags.Static);
                }

                type = null;

                object[] param = new object[] { property, type };

                FieldInfo fieldInfo = (FieldInfo)getFieldInfoFromPropertyInfo.Invoke(null, param);

                type = (Type)param[1];

                return fieldInfo;
            }
            catch (Exception)
            {
                throw new Exception("The method named 'GetFieldInfoFromProperty' in UnityEditor.ScriptAttributeUtility not found!");
            }
        }

        private static IDictionary ReorderableLists
        {
            get
            {
                try
                {
                    if (reorderableListsInfo == null)
                    {
                        reorderableListsInfo = Type.GetType("UnityEditor.PropertyHandler, UnityEditor.dll")
                            .GetField("s_reorderableLists", BindingFlags.Static | BindingFlags.NonPublic);
                    }

                    return (IDictionary)reorderableListsInfo.GetValue(null);
                }
                catch (Exception)
                {
                    throw new Exception("Failed to get value to the 's_reorderableLists' field in UnityEditor.PropertyHandler!");
                }
            }
        }

        private static Type ListWrapperType
        {
            get
            {
                try
                {
                    if (listWrapperType == null)
                    {
                        listWrapperType = Type.GetType("UnityEditorInternal.ReorderableListWrapper, UnityEditor.dll");
                    }

                    return listWrapperType;
                }
                catch (Exception)
                {
                    throw new Exception("Failed to get UnityEditorInternal.ReorderableListWrapper type!");
                }
            }
        }

        private static string GetPropertyIdentifier(SerializedProperty property)
        {
            Type listWrapperType = ListWrapperType;

            try
            {
                if (getPropertyIdentifierInfo == null)
                {
                    getPropertyIdentifierInfo = listWrapperType
                        .GetMethod("GetPropertyIdentifier", BindingFlags.Static | BindingFlags.Public);
                }

                return (string)getPropertyIdentifierInfo.Invoke(null, new object[] { property.Copy() });
            }
            catch (Exception)
            {
                throw new Exception("The method named 'GetPropertyIdentifier' in " + listWrapperType + " not found!");
            }
        }


        private static Gradient GetGradientValue(SerializedProperty property)
        {
            PropertyInfo propertyInfo = typeof(SerializedProperty).GetProperty("gradientValue",
                BindingFlags.NonPublic | BindingFlags.Instance);

            if (propertyInfo == null) return null;

            return propertyInfo.GetValue(property, null) as Gradient;
        }

        private static void SetGradientValue(this SerializedProperty property, Gradient gradient)
        {
            PropertyInfo propertyInfo = typeof(SerializedProperty).GetProperty("gradientValue",
                BindingFlags.NonPublic | BindingFlags.Instance);

            if (propertyInfo == null) return;

            propertyInfo.SetValue(property, gradient);
        }

        private static object GetGenericValue(SerializedProperty property)
        {
            GetFieldInfoFromProperty(property, out Type valueType);

            if (property.isArray)
            {
                if (valueType.IsArray)
                {
                    Array array = Array.CreateInstance(valueType.GetElementType(), property.arraySize);

                    for (int i = 0; i < array.Length; i++)
                        array.SetValue(GetValue(property.GetArrayElementAtIndex(i)), i);

                    return Convert.ChangeType(array, valueType);
                }
                else if (valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    IList list = Activator.CreateInstance(valueType) as IList;

                    for (int i = 0; i < property.arraySize; i++)
                        list.Add(GetValue(property.GetArrayElementAtIndex(i)));

                    return Convert.ChangeType(list, valueType);
                }
            }

            object value = Activator.CreateInstance(valueType);

            foreach (FieldInfo fieldInfo in valueType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (!fieldInfo.IsVisibleInInspector())
                    continue;

                SerializedProperty subProperty = property.FindPropertyRelative(fieldInfo.Name);

                if (subProperty == null)
                    continue;

                fieldInfo.SetValue(value, GetValue(subProperty));
            }

            return value;
        }

        private static void SetGenericValue(this SerializedProperty property, object value)
        {
            GetFieldInfoFromProperty(property, out Type valueType);

            if (property.isArray)
            {
                int currentSize = property.arraySize;
                int newSize = 0;
                List<object> newItems = new List<object>();

                if (valueType.IsArray)
                {
                    Array array = (Array)value;
                    newSize = array.Length;

                    for (int i = 0; i < newSize; i++)
                        newItems.Add(array.GetValue(i));
                }
                else if (valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    IList list = (IList)value;
                    newSize = list.Count;

                    for (int i = 0; i < newSize; i++)
                        newItems.Add(list[i]);
                }

                if (currentSize < newSize)
                {
                    for (int i = currentSize; i < newSize; i++)
                        property.InsertArrayElementAtIndex(i);
                }
                else if (currentSize > newSize)
                {
                    for (int i = currentSize - 1; i >= newSize; i--)
                        property.DeleteArrayElementAtIndex(i);
                }

                for (int i = 0; i < newSize; i++)
                    property.GetArrayElementAtIndex(i).SetValue(newItems[i]);
            }

            foreach (FieldInfo fieldInfo in valueType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (!fieldInfo.IsVisibleInInspector())
                    continue;

                SerializedProperty subProperty = property.FindPropertyRelative(fieldInfo.Name);

                if (subProperty == null)
                    continue;

                subProperty.SetValue(fieldInfo.GetValue(value));
            }
        }

        private static bool IsEqualDefault(object obj, Type objType)
        {
            if (objType.IsArray)
            {
                if (obj == null)
                    return true;

                return ((Array)obj).Length == 0;
            }

            if (objType.IsGenericType && objType.GetGenericTypeDefinition() == typeof(List<>))
            {
                if (obj == null)
                    return true;

                return ((IList)obj).Count == 0;
            }

            object defaultObj = GetDefaultValue(objType);

            if (defaultObj == null && obj == null)
                return true;

            if (objType.IsValueType && !objType.IsPrimitive)
            {
                foreach (FieldInfo fieldInfo in objType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    if (!fieldInfo.IsVisibleInInspector())
                        continue;

                    if (!IsEqualDefault(fieldInfo.GetValue(obj), fieldInfo.FieldType))
                        return false;
                }

                return true;
            }

            return obj.Equals(defaultObj);
        }

        private static object GetDefaultValue(Type type)
        {
            try
            {
                if (type.IsSubclassOf(typeof(UnityEngine.Object)))
                    return null;
                else if (type == typeof(String))
                    return string.Empty;
                else if (type.IsArray)
                    return Array.CreateInstance(type, 0);
                else
                    return Activator.CreateInstance(type);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}