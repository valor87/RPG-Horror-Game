using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
// makes a dropdown for the item that the player is hovering over
 public class itemDiscriptions
{
    public Image itemImage;
    public TextMeshProUGUI itemText;
}

public class itemMenu : MonoBehaviour
{
    public itemMenuEventCore itemEventCore; // the event core for just the items
    public GameObject itemHolder; // the parent of the item game objects
    public GameObject textToInstantiate; // makes the items for the play to select
    public CurrentItems playerItems; // the items that the player currently have
    public RectTransform selectionKnife;
    public RectTransform nextArrow;
    public RectTransform prevArrow;
    public KeyCode upKey = KeyCode.UpArrow;
    public KeyCode downKey = KeyCode.DownArrow;
    public KeyCode invokeButton = KeyCode.Space;
    public Vector3 knifeOffset; // the offset for the knife so its not behind the text
    public itemDiscriptions itemDescriptions; // the item image and the description ui
    public int childIndex; // the currently selected child
    public ItemsObjects itemToUse; // the item that the player wants to use

    int itemsForShowing = 0;
    int itemPage;
    private void OnEnable()
    {
        itemEventCore.EV_openItemMenu.Invoke();
        itemsForShowing = 0;
        instanciateItemList(0);
    }
    private void Awake()
    {
    }
    private void Start()
    {
        itemEventCore.EV_useItemOnHero.AddListener(useItemOnHero);
        itemEventCore.EV_closedMenu.AddListener(disableThisMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            disableThisMenu();
        }

        // take player input
        getKeyInput();
        // update the image and the text
        changeItemMessages();
    }
    private void changeItemMessages()
    {
        this.itemDescriptions.itemImage.sprite = playerItems.Items[itemPage + childIndex].ItemImage;
        this.itemDescriptions.itemText.text = playerItems.Items[itemPage + childIndex].Description;
    }
    private void getKeyInput()
    {
        GameObject invokeButtonGameObject = null;
        if (Input.GetKeyDown(upKey))
            childIndex--;
        if (Input.GetKeyDown(downKey))
            childIndex++;
        if (Input.GetKeyDown(invokeButton))
            invokeButtonGameObject = itemHolder.transform.GetChild(childIndex).gameObject;

        if (invokeButtonGameObject != null)
        {
            invokeButtonGameObject.GetComponent<Button>().onClick.Invoke();
            itemToUse = playerItems.Items[itemsForShowing + childIndex];
        }

        // visually changing the knifes location
        childIndex = Mathf.Clamp(childIndex, 0, itemHolder.transform.childCount - 1);
        RectTransform currentButtonTransform = itemHolder.transform.GetChild(childIndex).gameObject.GetComponent<RectTransform>();
        selectionKnifeLocation(currentButtonTransform);
    }
    void selectionKnifeLocation(RectTransform currentButton)
    {
        selectionKnife.position = currentButton.position + knifeOffset;
    }
    private void closeThisMenu()
    {
        itemEventCore.EV_closedMenu.Invoke();
        this.gameObject.SetActive(false);
    }
    private void useItemOnHero(PlayerStats heroStats)
    {
        int attackIncrease = 0 + itemToUse.AttackChange;
        int defenseIncrease = 0 + itemToUse.DefenseChange;
        int speedIncrese = 0 + itemToUse.SpeedChange;
        int healthIncrease = 0 + itemToUse.HpChange;

        heroStats.Attackstat += attackIncrease;
        heroStats.Defensestat += defenseIncrease;
        heroStats.Speedstat += speedIncrese;
        heroStats.Healthstat += healthIncrease;

        playerItems.Items.Remove(itemToUse);
        itemToUse = null;
        Destroy(itemHolder.transform.GetChild(childIndex).gameObject);
    }
    public void instanciateItemList(int itemPage)
    {
        clearItemChilds(itemHolder.transform);
        int startingItem = 5 * itemPage;
        childIndex = startingItem;
        this.itemPage = itemPage;
        itemsForShowing = playerItems.Items.Count - startingItem;
        float yOffset = 0;

        Debug.Log(itemsForShowing + " this is how many items that you want to show");

        for (int i = startingItem; i < playerItems.Items.Count; i++)
        {
            ItemsObjects var = playerItems.Items[i];
            if (yOffset == 4)
                break;

            GameObject tempText = Instantiate(textToInstantiate, itemHolder.transform);
            tempText.transform.position += new Vector3(0, -100 * yOffset, 0);
            tempText.name = var.Name;
            tempText.GetComponent<TextMeshProUGUI>().text = var.Name;
            yOffset++;
        }

        // leaving this here incase above doesnt work
        //foreach (ItemsObjects var in playerItems.Items)
        //{
        //    if (yOffset == 4)
        //        break;

        //    GameObject tempText = Instantiate(textToInstantiate, itemHolder.transform);
        //    tempText.transform.position += new Vector3(0, -100 * yOffset,0);
        //    tempText.name = var.Name;
        //    tempText.GetComponent<TextMeshProUGUI>().text = var.Name;
        //    yOffset++;
        //}
    }

    void clearItemChilds(Transform parent)
    {
        for (int child = 0; child < parent.childCount; child++)
        {
            Destroy(parent.GetChild(child).gameObject);
        }
    }
    void disableThisMenu()
    {
        gameObject.SetActive(false);
    }
}
