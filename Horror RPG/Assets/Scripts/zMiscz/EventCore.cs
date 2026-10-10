using UnityEngine;
using UnityEngine.Events;

public class EventCore : MonoBehaviour
{
    // for opening the overworld menu
    public UnityEvent<bool> EV_OpenCloseMenu; 
    // for when a item is picked up by the player
    public UnityEvent<ItemsObjects> EV_ItemPickUp;
    /* for when the player hits the action button
     * All listeners will check if they are the one that the player is 
     * trying to interact with
    */
    public UnityEvent<Vector3> EV_ObjectInteract;
}
