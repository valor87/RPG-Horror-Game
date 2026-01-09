namespace FoxalFace.Common.Editor
{
    using System;
    using System.Reflection;
    using UnityEditor;
    using UnityEditor.Experimental;
    using UnityEngine;


    public static class DrawerHelper
    {
        #region Constants
        public const string kArrayDataPattern = @"\.Array\.data\[\d+\]";
        #endregion

        #region Attributes
        private static PropertyInfo mixedValueContentInfo;

        private static FieldInfo indentPerLevelInfo;

        private static FieldInfo defaultFoldoutHeaderHeightInfo;

        private static FieldInfo arraySizeWidthInfo;

        private static FieldInfo onDrawInfo;

        private static ConstructorInfo drawStatesInfo;

        private static MethodInfo hasKeyFocusInfo;

        private static PropertyInfo windowToolbarHeightInfo;

        private static PropertyInfo openedFolderIconNameInfo;

        private static PropertyInfo toggleMixedStyleInfo;
        #endregion

        #region API
        public static float IndentPerLevel
        {
            get
            {
                try
                {
                    if (indentPerLevelInfo == null)
                    {
                        indentPerLevelInfo = typeof(EditorGUI)
                            .GetField("kIndentPerLevel", BindingFlags.Static | BindingFlags.NonPublic);
                    }

                    return (float)indentPerLevelInfo.GetValue(null);
                }
                catch (Exception)
                {
                    Debug.LogException(new Exception("Failed to get value to the 'kIndentPerLevel' field in EditorGUI!"));
                    return 15f;
                }
            }
        }

        public static float DefaultFoldoutHeaderHeight
        {
            get
            {
                try
                {
                    if (defaultFoldoutHeaderHeightInfo == null)
                    {
                        defaultFoldoutHeaderHeightInfo = Type.GetType("UnityEditorInternal.ReorderableListWrapper+Constants, UnityEditor.dll")
                            .GetField("kDefaultFoldoutHeaderHeight", BindingFlags.Static | BindingFlags.Public);
                    }

                    return (float)defaultFoldoutHeaderHeightInfo.GetValue(null);
                }
                catch (Exception)
                {
                    Debug.LogException(new Exception("Failed to get value to the 'kDefaultFoldoutHeaderHeight' field in UnityEditorInternal.ReorderableListWrapper+Constants!"));
                    return EditorGUIUtility.singleLineHeight;
                }
            }
        }

        public static float ArraySizeWidth
        {
            get
            {
                try
                {
                    if (arraySizeWidthInfo == null)
                    {
                        arraySizeWidthInfo = Type.GetType("UnityEditorInternal.ReorderableListWrapper+Constants, UnityEditor.dll")
                            .GetField("kArraySizeWidth", BindingFlags.Static | BindingFlags.Public);
                    }

                    return (float)arraySizeWidthInfo.GetValue(null);
                }
                catch (Exception)
                {
                    Debug.LogException(new Exception("Failed to get value to the 'kArraySizeWidth' field in UnityEditorInternal.ReorderableListWrapper+Constants!"));
                    return 48f;
                }
            }
        }

        public static float WindowToolbarHeight
        {
            get
            {
                try
                {
                    if (windowToolbarHeightInfo == null)
                    {
                        windowToolbarHeightInfo = Type.GetType("UnityEditor.ProjectBrowser, UnityEditor.dll")
                            .GetProperty("k_ToolbarHeight", BindingFlags.Static | BindingFlags.NonPublic);
                    }

                    return (float)windowToolbarHeightInfo.GetValue(null);
                }
                catch (Exception)
                {
                    Debug.LogException(new Exception("Failed to get value to the 'k_ToolbarHeight' property in ProjectBrowser!"));
                    return 21f;
                }
            }
        }

        public static string OpenedFolderIconName
        {
            get
            {
                try
                {
                    if (openedFolderIconNameInfo == null)
                    {
                        openedFolderIconNameInfo = typeof(EditorResources)
                            .GetProperty("openedFolderIconName", BindingFlags.Static | BindingFlags.NonPublic);
                    }

                    return (string)openedFolderIconNameInfo.GetValue(null);
                }
                catch (Exception)
                {
                    Debug.LogException(new Exception("Failed to get value to the 'openedFolderIconName' property in EditorResources!"));
                    return EditorGUIUtility.isProSkin ? "d_FolderOpened Icon" : "FolderOpened Icon";
                }
            }
        }

        public static GUIStyle ToggleMixedStyle
        {
            get
            {
                try
                {
                    if (toggleMixedStyleInfo == null)
                    {
                        toggleMixedStyleInfo = Type.GetType("UnityEditor.EditorGUIInternal, UnityEditor.dll")
                            .GetProperty("mixedToggleStyle", BindingFlags.Static | BindingFlags.NonPublic);
                    }

                    return (GUIStyle)toggleMixedStyleInfo.GetValue(null);
                }
                catch (Exception)
                {
                    Debug.LogException(new Exception("Failed to get value to the 'mixedToggleStyle' property in UnityEditor.EditorGUIInternal!"));
                    return EditorStyles.toggle;
                }
            }
        }

        public static GUIContent GetMixedValueContent()
        {
            try
            {
                if (mixedValueContentInfo == null)
                {
                    mixedValueContentInfo = typeof(EditorGUI)
                        .GetProperty("mixedValueContent", BindingFlags.Static | BindingFlags.NonPublic);
                }

                return (GUIContent)mixedValueContentInfo.GetValue(null);
            }
            catch (Exception)
            {
                throw new Exception("Failed to get value to the 'mixedValueContent' property in EditorGUI!");
            }
        }

        public static Rect GetArraySizeRect(Rect arrayRect)
        {
            return new Rect(arrayRect.xMax - ArraySizeWidth - IndentPerLevel * EditorGUI.indentLevel, arrayRect.y,
                ArraySizeWidth + IndentPerLevel * EditorGUI.indentLevel, DefaultFoldoutHeaderHeight);
        }

        public static bool DrawHighlightable(Rect position, GUIContent content, int controlID, GUIStyle style)
        {
            try
            {
                if (onDrawInfo == null)
                {
                    onDrawInfo = typeof(GUIStyle)
                        .GetField("onDraw", BindingFlags.Static | BindingFlags.NonPublic);
                }
            }
            catch (Exception)
            {
                throw new Exception("The 'onDraw' field in GUIStyle not found!");
            }

            object drawHandler = onDrawInfo.GetValue(null);

            if (drawHandler == null)
                return false;

            try
            {
                if (drawStatesInfo == null)
                {
                    drawStatesInfo = Type.GetType("UnityEngine.DrawStates, UnityEngine")
                        .GetConstructor(new Type[] { typeof(int), typeof(bool), typeof(bool), typeof(bool), typeof(bool) });
                }
            }
            catch (Exception)
            {
                throw new Exception("The 'DrawStates' constructor not found!");
            }

            try
            {
                if (hasKeyFocusInfo == null)
                {
                    hasKeyFocusInfo = typeof(GUIUtility)
                        .GetMethod("HasKeyFocus", BindingFlags.Static | BindingFlags.NonPublic);
                }
            }
            catch (Exception)
            {
                throw new Exception("The method named 'HasKeyFocus' in GUIUtility not found!");
            }

            object drawStates = drawStatesInfo.Invoke(new object[] {
                controlID,
                position.Contains(Event.current.mousePosition),
                false,
                false,
                hasKeyFocusInfo.Invoke(null, new object[] { controlID })
            });

            return (bool)drawHandler.GetType()
                .GetMethod("Invoke")
                .Invoke(drawHandler, new object[] { style, position, content, drawStates });
        }
        #endregion
    }
}