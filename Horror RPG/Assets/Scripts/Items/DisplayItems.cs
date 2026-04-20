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
    public ItemsObjects ItemPickup;
    public int changePage;
    

    List<GameObject> TextAssets = new List<GameObject>();
    EventCore eventcore;
    GameObject GameManager;
    Items Item;
    private void OnEnable()
    {
        
    }
    void GotItem(ItemsObjects item)
    {
        CurrentItems.Add(item);
        Item.PlayersItems = CurrentItems;
    }
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager = GameObject.Find("DontDestroyGameManager");
        Item = GameManager.GetComponent<CurrentItems>().items;
        CurrentItems = GameManager.GetComponent<CurrentItems>().Items;

        eventcore = GameManager.GetComponent<EventCore>();
    }
    public void SetUpItems(ItemsObjects arg)
    {
        DisplayMultiplyItems(CurrentItems, ItemList);
    }

    // Update is called once per frame
    void Update()
    {
        DisplayObject(TestItem);
    }
    private void DestroyChildren(GameObject Parent)
    {
        if(Parent.transform.childCount == 0)
        {
            Debug.Log($"Object {this.gameObject.name} has no children to destroy: Script {this.name} line: 58");
        }
        for (int i = 0; i < Parent.transform.childCount; i++)
        {
            Destroy(Parent.transform.GetChild(i).gameObject);
        }
    }
    private void DisplayMultiplyItems(List<ItemsObjects> AllItems, GameObject Display)
    {
        DestroyChildren(Display);
        TextAssets.Clear();
        int displayCount = 6;
        int previousItem = changePage * 6;
        for (int i = previousItem; i <= displayCount + previousItem; i++)
        {
            Debug.Log(i + " position in " + AllItems[i].Name);

            ItemsObjects Item = AllItems[i];
            GameObject _Item = Instantiate(TextAsset, Vector2.zero, Quaternion.identity, Display.transform);
            _Item.GetComponent<TextMeshProUGUI>().text = Item.Name;
            _Item.GetComponent<ItemContainer>().ThisItems = Item;
            _Item.SetActive(true);
            _Item.name = Item.Name;
            TextAssets.Add(_Item);
        }
    }

    public void changeItemPage(int changeValue)
    {
        changePage += changeValue;
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
