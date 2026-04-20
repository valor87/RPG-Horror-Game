using UnityEngine;
using UnityEngine.Events;

public class EventCore : MonoBehaviour
{
    // for opening the overworld menu
    public UnityEvent<bool> EV_OpenCloseMenu; 
    // for when a item is picked up by the player
    public UnityEvent<ItemsObjects> EV_ItemPickUp;
}
