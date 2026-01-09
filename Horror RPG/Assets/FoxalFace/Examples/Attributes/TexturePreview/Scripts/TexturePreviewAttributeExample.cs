namespace FoxalFace.Attributes.Example
{
    using FoxalFace.Attributes;
    using UnityEngine;


    /// <summary>
    /// Example demonstrating the [TexturePreview] attribute.
    /// It previews assigned textures or sprites directly in the Inspector,
    /// and optionally shows the asset path.
    /// </summary>
    public class TexturePreviewAttributeExample : MonoBehaviour
    {
        #region UI Icons
        [Header("UI Icons")]

        [Tooltip("Main icon displayed in the UI.")]
        [SerializeField, TexturePreview]
        private Texture2D mainIcon;

        [Tooltip("Alternative icon for hovered state.")]
        [SerializeField, TexturePreview(visiblePath: false)]
        private Texture2D hoverIcon;
        #endregion

        #region Inventory Sprites
        [Header("Inventory Sprites")]

        [Tooltip("Thumbnail used in the inventory grid.")]
        [SerializeField, TexturePreview]
        private Sprite itemThumbnail;

        [Tooltip("Rarity overlay shown behind the item.")]
        [SerializeField, TexturePreview]
        private Sprite rarityFrame;
        #endregion
    }
}
