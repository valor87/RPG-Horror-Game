using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class itemMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public itemMenuEventCore itemEventCore;
    public GameObject textToInstantiate;
    public CurrentItems playerItems;

    private void OnEnable()
    {
        itemEventCore.EV_openItemMenu.Invoke();
        instanciateItemList();
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

    private void instanciateItemList()
    {
        Debug.Log(playerItems.Items[0]);
        float yOffset = 0;
        foreach (ItemsObjects var in playerItems.Items)
        {
            GameObject tempText = Instantiate(textToInstantiate,transform);
            tempText.transform.position += new Vector3(0, -100 * yOffset,0);
            tempText.name = var.Name;
            tempText.GetComponent<TextMeshProUGUI>().text = var.Name;
            yOffset++;
        }
    }
}
