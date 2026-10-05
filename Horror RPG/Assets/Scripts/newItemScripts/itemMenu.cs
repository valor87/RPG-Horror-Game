using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
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
    bool endCombatWait;
    int itemsForShowing = 0;
    int itemPage;
    Vector3 knifeOffsetForNextArrows = new(-80, -3.7f, 0);

    public IEnumerator pauseCombatScene()
    {
        endCombatWait = true;
        while (endCombatWait)
        {
            yield return null;
        }
    }

    private void OnEnable()
    {
        // opens the items menu right off the top for some reason
        itemEventCore.EV_openItemMenu.Invoke();
        itemsForShowing = 0;
        instanciateItemList();
    }
    private void Awake()
    {
    }
    private void Start()
    {
        itemEventCore.EV_useItemOnHero.AddListener(useItemOnHero);
        itemEventCore.EV_closedMenu.AddListener(disableThisMenu);
        itemEventCore.EV_closedMenu.AddListener(enableScript);
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
        
        // theres a bug where if the player is looking at the arrows then thes a range error
        try
        {
            this.itemDescriptions.itemImage.sprite = playerItems.Items[(itemPage * 5) + childIndex].ItemImage;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            this.itemDescriptions.itemImage.sprite = null;
            this.itemDescriptions.itemText.text = "No item selected";
            return;
        }
        // change the image if the player is looking at the arrows
        if (itemHolder.transform.GetChild(childIndex).CompareTag("arrow"))
        {
            this.itemDescriptions.itemImage.sprite = null;
            this.itemDescriptions.itemText.text = "No item selected";
            return;
        }
        // set the image and the text to the item that the player is looking at
        this.itemDescriptions.itemImage.sprite = playerItems.Items[(itemPage * 5) + childIndex].ItemImage;
        this.itemDescriptions.itemText.text = playerItems.Items[(itemPage * 5) + childIndex].Description;
    }
    private void getKeyInput()
    {
        GameObject invokeButtonGameObject = null;
        if (Input.GetKeyDown(upKey))
            childIndex--;
        if (Input.GetKeyDown(downKey))
            childIndex++;
        if (Input.GetKeyDown(invokeButton))
        {
            invokeButtonGameObject = itemHolder.transform.GetChild(childIndex).gameObject;
        }
        if (invokeButtonGameObject != null)
        {
            invokeButtonGameObject.GetComponent<Button>().onClick.Invoke();
            if (invokeButtonGameObject.CompareTag("arrow"))
                return;
            itemToUse = playerItems.Items[itemPage + childIndex];
        }

        // visually changing the knifes location
        childIndex = Mathf.Clamp(childIndex, 0, itemHolder.transform.childCount - 1);
        RectTransform currentButtonTransform = itemHolder.transform.GetChild(childIndex).gameObject.GetComponent<RectTransform>();
        selectionKnifeLocation(currentButtonTransform);
    }
    void selectionKnifeLocation(RectTransform currentButton)
    {
        if (currentButton.gameObject.CompareTag("arrow"))
        {
            selectionKnife.position = currentButton.position + knifeOffsetForNextArrows;
            return;
        }

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
        int currentHealthIncrease = 0 + itemToUse.CurrentHpChange;
        heroStats.Attackstat += attackIncrease;
        heroStats.Defensestat += defenseIncrease;
        heroStats.Speedstat += speedIncrese;
        heroStats.Healthstat += healthIncrease;
        heroStats.CurrentHealth += currentHealthIncrease;

        playerItems.Items.Remove(itemToUse);
        itemToUse = null;
        Destroy(itemHolder.transform.GetChild(childIndex).gameObject);
    }
    /// <summary>
    /// displays all the varibles when the items menu gets open
    /// </summary>
    private void instanciateItemList()
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

        cloneNextArrows();
        
    }
    void cloneNextArrows()
    {
        GameObject leftArrowClone = Instantiate(nextArrow.gameObject, itemHolder.transform);
        GameObject rightArrowClone = Instantiate(prevArrow.gameObject, itemHolder.transform);
    }
    void clearItemChilds(Transform parent)
    {
        for (int child = 0; child < parent.childCount; child++)
        {
            Destroy(parent.GetChild(child).gameObject);
        }
    }
    public void increaseItemPage(int changeAmount)
    {
        Debug.Log(((itemPage + changeAmount) * 4) - 3);
        if ((((itemPage + changeAmount) * 4)) > playerItems.items.PlayersItems.Count)
            return;

        itemPage += changeAmount;
        instanciateItemList();
    }
    public void decreaseItemPage(int changeAmount)
    {
        if (itemPage - changeAmount < 0)
            return;

        itemPage -= changeAmount;
        instanciateItemList();
    }
    void disableThisMenu()
    {
        endCombatWait = false;
        gameObject.SetActive(false);
        instanciateItemList();
    }

    void enableScript()
    {
        this.enabled = true;
    }
}
