namespace FoxalFace.Attributes.Editor
{
    using System;
    using FoxalFace.Common.Editor;
    using UnityEditor;
    using UnityEngine;


    [CustomPropertyDrawer(typeof(TexturePreviewAttribute), true)]
    public class TexturePreviewDrawer : PropertyDrawer
    {
        #region Constants
        private const float kSize = 64f;

        private const float kSpacing = 3f;

        private static readonly int kTexturePreviewHash = "s_TexturePreview".GetHashCode();

        private static GUIStyle kPrefixStyle = new GUIStyle(EditorStyles.label) { alignment = TextAnchor.UpperLeft };

        private static GUIStyle kPathBackgroundStyle = new GUIStyle(EditorStyles.helpBox)
        {
            fontStyle = FontStyle.Normal,
            focused = new GUIStyleState()
            {
                background = EditorStyles.helpBox.focused.background,
                scaledBackgrounds = EditorStyles.helpBox.focused.scaledBackgrounds,
                textColor = EditorStyles.label.focused.textColor
            }
        };

        private static GUIStyle kPathContentStyle = new GUIStyle(EditorStyles.label)
        {
            fontSize = EditorStyles.helpBox.fontSize,
            fontStyle = FontStyle.Normal,
            padding = new RectOffset(EditorStyles.helpBox.padding.left, EditorStyles.helpBox.padding.right, 0, 0)
        };

        private static GUIContent kPathBackgroundContent = new GUIContent("Path:");
        #endregion

        #region Attributes
        private Vector2 scrollPosition = new Vector2(-1f, 0f);
        #endregion

        #region Unity Methods
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.IsTextureOrSprite(out Type _))
            {
                if (property.IsArrayElement())
                    return kSize + 2;

                return kSize;
            }
            else
                return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            try
            {
                if (property.IsTextureOrSprite(out Type valueType))
                {
                    label = EditorGUI.BeginProperty(position, label, property);

                    EditorGUI.showMixedValue = property.hasMultipleDifferentValues;

                    if (property.IsArrayElement())
                        position.height -= 2;

                    TexturePreviewAttribute textPreviewAttr = (TexturePreviewAttribute)attribute;

                    string assetPath = "";

                    if (EditorGUI.showMixedValue)
                        assetPath = DrawerHelper.GetMixedValueContent().text;
                    else if (textPreviewAttr.VisiblePath && property.objectReferenceValue)
                        assetPath = AssetDatabase.GetAssetPath(property.objectReferenceValue).Replace("/", " > ");

                    float labelWidth = EditorGUIUtility.labelWidth;
                    float lineHeight = TexturePreviewDrawerUtility.EditorGUILineHeight;

                    int id = GUIUtility.GetControlID(kTexturePreviewHash, FocusType.Keyboard, position);

                    EditorGUIUtility.labelWidth = position.width - kSize - kSpacing;
                    TexturePreviewDrawerUtility.EditorGUILineHeight = position.height;

                    Rect fieldRect = EditorGUI.PrefixLabel(position, id, label, kPrefixStyle);

                    EditorGUIUtility.labelWidth = labelWidth;
                    TexturePreviewDrawerUtility.EditorGUILineHeight = lineHeight;

                    fieldRect.x = fieldRect.xMax - kSize;
                    fieldRect.width = kSize;

                    Rect pathRect = new Rect(position);
                    if (textPreviewAttr.VisiblePath)
                    {
                        pathRect.width -= kSize + kSpacing;
                        pathRect.y += EditorGUIUtility.singleLineHeight;
                        pathRect.height -= EditorGUIUtility.singleLineHeight;
                    }

                    Rect pathViewportRect = new Rect(pathRect);
                    if (textPreviewAttr.VisiblePath)
                    {
                        pathViewportRect.x += 1;
                        pathViewportRect.width -= 2;
                        pathViewportRect.y += 17f;
                        pathViewportRect.height -= 18f;
                    }

                    Rect contentRect = Rect.zero;
                    if (textPreviewAttr.VisiblePath)
                    {
                        contentRect = new Rect(Vector2.zero, kPathContentStyle.CalcSize(new GUIContent(assetPath)));

                        if (scrollPosition.x < 0)
                        {
                            if (contentRect.width > pathViewportRect.width)
                                scrollPosition = new Vector2(contentRect.width - pathViewportRect.width, 0f);
                            else
                                scrollPosition = Vector2.zero;
                        }

                        EditorGUI.BeginChangeCheck();
                    }

                    TexturePreviewDrawerUtility.DoObjectField(fieldRect, position, id, valueType, property, false);

                    if (textPreviewAttr.VisiblePath)
                    {
                        if (EditorGUI.EndChangeCheck())
                            if (contentRect.width > pathViewportRect.width)
                                scrollPosition = new Vector2(contentRect.width - pathViewportRect.width, 0f);

                        if (Event.current.type == EventType.Repaint)
                        {
                            GUIContent backgroundContent = new GUIContent(kPathBackgroundContent);
                            if (EditorGUI.showMixedValue)
                                backgroundContent.tooltip = DrawerHelper.GetMixedValueContent().tooltip;

                            if (!DrawerHelper.DrawHighlightable(pathRect, backgroundContent, id, kPathBackgroundStyle))
                                kPathBackgroundStyle.Draw(pathRect, backgroundContent, id);
                        }

                        bool prevEnable = GUI.enabled;

                        GUI.enabled = true;
                        scrollPosition = GUI.BeginScrollView(pathViewportRect, scrollPosition, contentRect, false, false);
                        GUI.enabled = prevEnable;

                        if (Event.current.type == EventType.Repaint)
                        {
                            GUIContent pathContent = new GUIContent(assetPath);

                            GUI.enabled = !EditorGUI.showMixedValue && prevEnable;

                            if (EditorGUI.showMixedValue)
                                kPathContentStyle.Draw(contentRect, pathContent, id);
                            else if (!DrawerHelper.DrawHighlightable(contentRect, pathContent, id, kPathContentStyle))
                                kPathContentStyle.Draw(contentRect, pathContent, id);

                            GUI.enabled = prevEnable;
                        }

                        GUI.enabled = true;
                        GUI.EndScrollView();
                        GUI.enabled = prevEnable;

                        pathRect.x += 2;
                        pathRect.y += 2;
                        pathRect.width -= 4;
                        pathRect.height -= 4;

                        if (!EditorGUI.showMixedValue && Event.current.type == EventType.MouseDown && pathRect.Contains(Event.current.mousePosition) && property.objectReferenceValue)
                        {
                            if (Event.current.clickCount == 1)
                                EditorGUIUtility.PingObject(property.objectReferenceValue);
                            else if (Event.current.clickCount == 2)
                                AssetDatabase.OpenAsset(property.objectReferenceValue);
                        }

                        EditorGUI.showMixedValue = false;
                    }

                    EditorGUI.EndProperty();
                }
                else
                    EditorGUI.PropertyField(position, property, label, true);
            }
            catch (Exception e)
            {
                Debug.LogException(e);

                EditorGUI.PropertyField(position, property, label, true);
            }
        }
        #endregion
    }
}