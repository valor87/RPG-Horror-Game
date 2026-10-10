using UnityEngine;
using System.Collections.Generic;
public class CurrentItems : MonoBehaviour
{
    public List<ItemsObjects> Items;
    public Items items = new Items();
    EventCore _EventCore;
    public void Start()
    {
        items.PlayersItems = GetComponent<CurrentItems>().Items;
        _EventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        _EventCore.EV_ItemPickUp.AddListener(AddItemToInventory);
    }

    void AddItemToInventory(ItemsObjects item)
    {
        Items.Add(item);
    }

}
