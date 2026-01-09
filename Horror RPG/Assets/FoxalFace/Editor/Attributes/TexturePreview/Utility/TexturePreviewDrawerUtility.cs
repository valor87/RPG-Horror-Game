namespace FoxalFace.Attributes.Editor
{
    using System;
    using System.Linq;
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;


    internal static class TexturePreviewDrawerUtility
    {
        #region Attributes
        private static PropertyInfo lineHeightInfo;

        private static MethodInfo doObjectFieldInfo;
        #endregion

        #region API
        internal static float EditorGUILineHeight
        {
            get
            {
                try
                {
                    if (lineHeightInfo == null)
                    {
                        lineHeightInfo = typeof(EditorGUI)
                            .GetProperty("lineHeight", BindingFlags.Static | BindingFlags.NonPublic);
                    }

                    return (float)lineHeightInfo.GetValue(null);
                }
                catch (Exception)
                {
                    Debug.LogException(new Exception("Failed to get value to the 'lineHeight' property in EditorGUI!"));
                    return EditorGUIUtility.singleLineHeight;
                }
            }

            set
            {
                try
                {
                    if (lineHeightInfo == null)
                    {
                        lineHeightInfo = typeof(EditorGUI)
                            .GetProperty("lineHeight", BindingFlags.Static | BindingFlags.NonPublic);
                    }

                    lineHeightInfo.SetValue(null, value);
                }
                catch (Exception)
                {
                    Debug.LogException(new Exception("Failed to set value to the 'lineHeight' property in EditorGUI!"));
                }
            }
        }

        internal static void DoObjectField(Rect position, Rect dropRect, int id, Type objType, SerializedProperty property, bool allowSceneObjects)
        {
            try
            {
                if (doObjectFieldInfo == null)
                {
                    doObjectFieldInfo = typeof(EditorGUI).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
                        .Where(m => m.Name == "DoObjectField" && m.GetParameters().Count() == 8 && m.GetParameters()[4].ParameterType == typeof(SerializedProperty))
                        .First();
                }

                doObjectFieldInfo.Invoke(null, new object[] { position, dropRect, id, objType, property, null, allowSceneObjects, EditorStyles.objectField });
            }
            catch (TargetInvocationException tie)
            {
                if (tie.InnerException is ExitGUIException)
                {
                    // Nothing to do
                }
                else
                {
                    Debug.LogException(tie.InnerException);
                    throw new Exception("Failed to invoke method 'DoObjectField' in EditorGUI!", tie.InnerException);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw new Exception("Failed to invoke method 'DoObjectField' in EditorGUI!", e);
            }
        }
        #endregion
    }
}
