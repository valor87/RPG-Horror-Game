namespace FoxalFace.Attributes
{
    using System;
    using UnityEngine;


    /// <summary>
    /// Attribute displaying a texture preview.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class TexturePreviewAttribute : PropertyAttribute
    {
        public readonly bool VisiblePath;

        public TexturePreviewAttribute(bool visiblePath = true)
        {
            order = Constants.kTexturePreviewOrder;
            VisiblePath = visiblePath;
        }
    }
}