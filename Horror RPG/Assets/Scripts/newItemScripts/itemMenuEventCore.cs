using UnityEngine;
using UnityEngine.Events;

public class itemMenuEventCore : MonoBehaviour
{
    public UnityEvent EV_openItemMenu;

    public UnityEvent EV_openHeroMenu;

    public UnityEvent EV_closedMenu;

    public UnityEvent<PlayerStats> EV_useItemOnHero;
}
