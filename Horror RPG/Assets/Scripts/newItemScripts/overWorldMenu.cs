using UnityEngine;

public class overWorldMenu : MonoBehaviour
{
    public KeyCode openMenu = KeyCode.Tab;
    EventCore eventCore;
    bool menustate = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
    }

    // Update is called once per frame
    void Update()
    {
        keyboardInput();
    }
    /// <summary>
    /// for opening and closing the over world menu
    /// </summary>
    void keyboardInput()
    {
        if (Input.GetKeyDown(openMenu)){
            menustate = !menustate;
            eventCore.EV_OpenCloseMenu.Invoke(menustate);
        }

    }
}
