/*
this script will go through all the children attached to this object
and give the player the ability to call the button on click value
*/

using UnityEngine;
using UnityEngine.UI;

public class callChildButtons : MonoBehaviour
{
    public KeyCode upKey = KeyCode.UpArrow;
    public KeyCode downKey = KeyCode.DownArrow;
    public KeyCode invokeButton = KeyCode.Space;
    
    int childIndex;
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

        childIndex = Mathf.Clamp(childIndex, 0, transform.childCount -1);

        if (invokeButtonGameObject != null)
        {
            invokeButtonGameObject.GetComponent<Button>().onClick.Invoke();
        }
    
    }

}
