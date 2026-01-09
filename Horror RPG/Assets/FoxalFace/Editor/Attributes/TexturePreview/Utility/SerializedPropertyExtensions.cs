namespace FoxalFace.Attributes.Editor
{
    using System;
    using FoxalFace.Common.Editor;
    using UnityEditor;
    using UnityEngine;


    internal static partial class SerializedPropertyExtensions
    {
        #region API
        internal static bool IsTextureOrSprite(this SerializedProperty property, out Type valueType)
        {
            property.GetFieldInfo(out valueType);

            if (property.propertyType != SerializedPropertyType.ObjectReference)
                return false;

            // Accept Texture and derived types (e.g., Texture2D) and Sprite
            return typeof(Texture).IsAssignableFrom(valueType) || valueType == typeof(Sprite);
        }
        #endregion
    }
}