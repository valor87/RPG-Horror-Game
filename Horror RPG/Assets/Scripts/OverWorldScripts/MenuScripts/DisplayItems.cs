using System.Collections.Generic;
using TMPro;
using UnityEditor;
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
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisplayMultiplyItems(CurrentItems, ItemList);
    }

    // Update is called once per frame
    void Update()
    {
        DisplayInfoItem(Image, TextDescription, TextAsset.GetComponent<TextMeshProUGUI>());
    }
    private void DisplayMultiplyItems(List<ItemsObjects> AllItems, GameObject Display)
    {
        foreach (ItemsObjects Item in AllItems)
        {
            GameObject _Item = Instantiate(TextAsset, Vector2.zero, Quaternion.identity, Display.transform);
            _Item.GetComponent<TextMeshProUGUI>().text = Item.Name;
        }
    }
    
    void DisplayInfoItem(Image ImageOutput, TextMeshProUGUI TextOutPut, TextMeshProUGUI NameOutPut)
    {
        NameOutPut.GetComponent<TextMeshProUGUI>().text = TestItem.Name;
        ImageOutput.sprite = TestItem.ItemImage;
        TextOutPut.text = TestItem.Description;
    }
}
