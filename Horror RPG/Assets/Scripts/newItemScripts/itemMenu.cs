using UnityEngine;
using UnityEngine.Rendering;

public class itemMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public itemMenuEventCore itemEventCore;


    private void OnEnable()
    {
        itemEventCore.EV_openItemMenu.Invoke();
        Debug.Log("The item menu is open");
    }

    private void OnDisable()
    {
        closeThisMenu();
        Debug.Log("The item menu is closed");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            this.gameObject.SetActive(false);
        }
    }

    private void closeThisMenu()
    {
        itemEventCore.EV_closedMenu.Invoke();
        this.gameObject.SetActive(false);
    }
}
