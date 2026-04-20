using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class itemMenu : MonoBehaviour
{
    public itemMenuEventCore itemEventCore;
    public GameObject itemHolder;
    public GameObject textToInstantiate;
    public CurrentItems playerItems;
    public RectTransform selectionKnife;
    public KeyCode upKey = KeyCode.UpArrow;
    public KeyCode downKey = KeyCode.DownArrow;
    public KeyCode invokeButton = KeyCode.Space;
    public Vector3 knifeOffset;
    int childIndex;
    public ItemsObjects itemToUse;
    private void OnEnable()
    {
        itemEventCore.EV_openItemMenu.Invoke();
        Debug.Log("The item menu is open");
    }
    private void Awake()
    {
        instanciateItemList();
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
            invokeButtonGameObject = itemHolder.transform.GetChild(childIndex).gameObject;

        if (invokeButtonGameObject != null)
        {
            invokeButtonGameObject.GetComponent<Button>().onClick.Invoke();
            itemToUse = playerItems.Items[childIndex];
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
    private void instanciateItemList()
    {
        Debug.Log(playerItems.Items[0]);
        float yOffset = 0;
        foreach (ItemsObjects var in playerItems.Items)
        {
            GameObject tempText = Instantiate(textToInstantiate, itemHolder.transform);
            tempText.transform.position += new Vector3(0, -100 * yOffset,0);
            tempText.name = var.Name;
            tempText.GetComponent<TextMeshProUGUI>().text = var.Name;
            yOffset++;
        }
    }


    void disableThisMenu()
    {
        gameObject.SetActive(false);
    }
}
