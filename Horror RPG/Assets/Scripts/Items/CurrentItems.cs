using UnityEngine;
using System.Collections.Generic;
public class CurrentItems : MonoBehaviour
{
    public List<ItemsObjects> Items;
    public Items items = new Items();

    public void Start()
    {
        items.PlayersItems = GetComponent<CurrentItems>().Items;
    }
}
