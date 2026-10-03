/*
this script will go through all the children attached to this object
and give the player the ability to call the button on click value
*/

using UnityEngine;
using UnityEngine.UI;

public class callChildButtons : MonoBehaviour
{
    public bool mainMenu = false;
    public itemMenuEventCore itemMenuEventCore;
    public EventCore EventCore;
    public KeyCode upKey = KeyCode.UpArrow;
    public KeyCode downKey = KeyCode.DownArrow;
    public KeyCode invokeButton = KeyCode.Space;
    public RectTransform selectionKnife;
    public Vector3 knifeOffset;
    int childIndex;
    public bool notCurrentMenu;
    private void Start()
    {
        EventCore.EV_OpenCloseMenu.AddListener(enableThis);
        itemMenuEventCore.EV_closedMenu.AddListener(turnOnKeyboardInput);
    }

    void Update()
    {
        if (!notCurrentMenu) {
            getKeyInput();
        } 
    }

    private void getKeyInput()
    {
        GameObject invokeButtonGameObject = null;
        if (Input.GetKeyDown(upKey))
            childIndex--;
        if (Input.GetKeyDown(downKey))
            childIndex++;
        if (Input.GetKeyDown(invokeButton))
           invokeButtonGameObject = transform.GetChild(childIndex).gameObject;

        if (invokeButtonGameObject != null)
        {
            invokeButtonGameObject.GetComponent<Button>().onClick.Invoke();
        }

        // visually changing the knifes location
        childIndex = Mathf.Clamp(childIndex, 0, transform.childCount - 1);
        RectTransform currentButtonTransform = transform.GetChild(childIndex).gameObject.GetComponent<RectTransform>();
        selectionKnifeLocation(currentButtonTransform);
    }
    void selectionKnifeLocation(RectTransform currentButton)
    {
        selectionKnife.position = currentButton.position + knifeOffset;
    }
    // for making the keyboard work for the first set of menus
    public void stopKeyboardMovement(bool state)
    {
        notCurrentMenu = state;
    }
    // dude without remaking everything this makes the table also turn 
    // the keyboard state on
    private void enableThis(bool state)
    {
        state = !state;
        stopKeyboardMovement(state);
    }
    // Just because the person who made this is super dum
    // this makes the x key or returning from a sub menu turn the menu function back on
    // throw me into a lake with rocks around my neck
    private void turnOnKeyboardInput()
    {
        stopKeyboardMovement(false);
    }
}
