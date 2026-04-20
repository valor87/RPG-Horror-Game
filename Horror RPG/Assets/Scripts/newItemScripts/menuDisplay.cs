using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class menuDisplay : MonoBehaviour
{
    EventCore eventCore;
    public itemMenuEventCore itemEventCore;
    public List<GameObject> subMenus = new List<GameObject>();
    public KeyCode closeSubMenusKey = KeyCode.X;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.EV_OpenCloseMenu.AddListener(showMenu);
        itemEventCore.EV_closedMenu.AddListener(disableThisMenu);
    }

    private void Update()
    {
        closeAllSubMenus();
    }
    void closeAllSubMenus()
    {
        if (Input.GetKeyDown(closeSubMenusKey))
        {
            itemEventCore.EV_closedMenu.Invoke();
        } 
    }
    /// <summary>
    /// set and deactivate the overworld menu
    /// </summary>
    /// <param name="state"></param>
    void showMenu(bool state)
    {
        for (int i = 0; i <= transform.childCount - 1; i++)
        {
            transform.GetChild(i).gameObject.SetActive(state);
        }
    }

    void disableThisMenu()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
