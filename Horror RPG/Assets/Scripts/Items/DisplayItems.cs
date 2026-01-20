using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayItems : MonoBehaviour
{
    public ItemsObjects TestItem;
    public GameObject ItemList;
    public GameObject TextAsset;

    public Image Image;
    public TextMeshProUGUI TextDescription;

    public List<ItemsObjects> CurrentItems;
    List<GameObject> TextAssets = new List<GameObject>();
    GameObject GameManager;
    Items Item;
    private void OnEnable()
    {
        GameManager = GameObject.Find("DontDestroyGameManager");
        Item = GameManager.GetComponent<CurrentItems>().items;
        CurrentItems = Item.PlayersItems;
    }
    public void SetUpItems()
    {
        DisplayMultiplyItems(CurrentItems, ItemList);

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    private void DisplayMultiplyItems(List<ItemsObjects> AllItems, GameObject Display)
    {
        foreach (ItemsObjects Item in AllItems)
        {
            GameObject _Item = Instantiate(TextAsset, Vector2.zero, Quaternion.identity, Display.transform);
            _Item.GetComponent<TextMeshProUGUI>().text = Item.Name;
            _Item.GetComponent<ItemContainer>().ThisItems = Item;
            _Item.SetActive(true);
            _Item.name = Item.Name;
            TextAssets.Add(_Item);
        }
    }
    public void UseItem(ItemsObjects Remove)
    {
        for (int i = 0; i < CurrentItems.Count; i++)
        {
            if (Remove == TextAssets[i].GetComponent<ItemContainer>().ThisItems)
            {
                TextAssets.Remove(TextAssets[i]);
                //Destroy(TextAssets[i]);
                Item.RemoveItem(Remove);
                break;
            }
        }
       
    }
    public void DisplayObject(ItemsObjects CurrentSelectedItem)
    {
        DisplayInfoItem(Image, TextDescription, TextAsset.GetComponent<TextMeshProUGUI>(), CurrentSelectedItem);
    }
    void DisplayInfoItem(Image ImageOutput, TextMeshProUGUI TextOutPut, TextMeshProUGUI NameOutPut, ItemsObjects Item)
    {
        NameOutPut.GetComponent<TextMeshProUGUI>().text = Item.Name;
        ImageOutput.sprite = Item.ItemImage;
        TextOutPut.text = Item.Description;
    }
}
