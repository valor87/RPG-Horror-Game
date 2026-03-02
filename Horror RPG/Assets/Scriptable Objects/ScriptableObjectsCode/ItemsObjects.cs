using UnityEngine;
using FoxalFace.Attributes;
public enum Type
{
    Potion,
    Weapon,
    Stat_Increase,
    Key_Item,
}
[CreateAssetMenu(fileName = "ItemsObjects", menuName = "Scriptable Objects/ItemsObjects")]

public class ItemsObjects : ScriptableObject
{
    [Tooltip("Name of the item")]
    public string Name;
    [Tooltip("The type of the object")]
    public Type Type;
    [Tooltip("The change amount to the hp")]
    public int HpChange;
    [Tooltip("The change amount to the attack")]
    public int AttackChange;
    [Tooltip("The change amount to the Defense")]
    public int DefenseChange;
    [Tooltip("The change amount to the speed")]
    public int SpeedChange;
    [Tooltip("How much the item costs")]
    public int Cost;
    [Tooltip("How many times the item can be used before it disappears")]
    public int Uses;
    [Tooltip("The item description for the player to read")]
    public string Description;
    [TexturePreview]
    public Sprite ItemImage;
}
