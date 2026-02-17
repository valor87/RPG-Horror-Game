using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;
using Unity.VisualScripting;
public class Items
{
    public List<ItemsObjects> PlayersItems;

    /// <summary>
    /// For finding if a player has a certain item
    /// </summary>
    /// <param name="ToFind"></param>
    /// <returns></returns>
    public bool HasItem(ItemsObjects ToFind)
    {
        foreach (ItemsObjects _Var in PlayersItems)
        {
            if (_Var == ToFind)
            {
                return true;
            }
            continue;
        }
        return false;
    }
    /// <summary>
    /// Seach the players current itmes for a desired item.
    /// Sends a debug message if it fails
    /// </summary>
    /// <param name="ToBeRemoved"></param>
    public void RemoveItem(ItemsObjects ToBeRemoved)
    {
        foreach (ItemsObjects _Var in PlayersItems)
        {
            if (_Var == ToBeRemoved)
            {
                PlayersItems.Remove(_Var);
                return;
            }
        }
        Debug.Log($"Faild to find {ToBeRemoved} item form players current items");
    }
}
