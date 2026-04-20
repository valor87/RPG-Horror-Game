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
    public KeyCode upKey = KeyCode.UpArrow;
    public KeyCode downKey = KeyCode.DownArrow;
    public KeyCode invokeButton = KeyCode.Space;
    public RectTransform selectionKnife;
    public Vector3 knifeOffset;
    int childIndex;

    private void Start()
    {
        if(mainMenu)
            itemMenuEventCore.EV_closedMenu.AddListener(enableThis);
    }

    void Update()
    {
        getKeyInput();
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
            this.enabled = false;
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

    private void enableThis()
    {
        this.enabled = true;
    }
}
