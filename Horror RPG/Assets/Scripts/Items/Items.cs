using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;
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
}
