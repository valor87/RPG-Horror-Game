using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
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
        TextAsset.GetComponent<TextMeshProUGUI>().text = TestItem.Name;
        Image.sprite = TestItem.ItemImage;
        TextDescription.text = TestItem.Description;
        DisplayMultiplyItems(CurrentItems, ItemList);
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
        }
    } 
}
