using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OverWorldMenuNavagation : MonoBehaviour
{
    /// <summary>
    /// show players money
    /// enemies killed
    /// </summary>
    public OverWorldPlayerMovement InScenePlayerScript;
    [Header("Getting Parents to all interactable objects")]
    public GameObject FirstMenuParent;
    public GameObject ButtonsParent;
    public GameObject HeroStatsParent;
    public GameObject ItemListParent;
    public GameObject ConfirmMenu;
    // Buttons list
    public List<GameObject> MainButtons = new List<GameObject>();

    // misc menu refs
    List<GameObject> MenuChildren = new List<GameObject>();
    public List<GameObject> CurrentMenu = new List<GameObject>();
    public List<GameObject> ItemsMenu = new List<GameObject>();
    List<GameObject> ConfirmMenuOptions = new List<GameObject>();
    List<GameObject> HeroStatsMenu = new List<GameObject>();
    // menu navagation
    bool ShowMenu;
    int posinlist;
    [Space(5)]
    [Header("For selecting menu options")]
    public GameObject UiSelectionKnife;
    public Vector3 knifeoffset = new Vector3(100, 0, 0); // offset for the knife in UI

    // Display item Script
    public DisplayItems DisplayItems;
    ItemsObjects ItemToUse;
    PlayerStats PlayerToUse;
    GameObject usedItemText;
    void Start()
    {
        DisplayItems.SetUpItems(null);
        ItemsMenu = SetUpListFromParent(ItemListParent);
        MenuChildren = SetUpListFromParent(FirstMenuParent.gameObject);
        MainButtons = SetUpListFromParent(ButtonsParent);
        HeroStatsMenu = SetUpListFromParent(HeroStatsParent);
        CurrentMenu = MainButtons;
    }

    // Update is called once per frame
    void Update()
    {
        SelectionMovement();
        SetPlayerState(!ShowMenu);
        
    }
    private void LateUpdate()
    {
        if (CurrentMenu == ItemsMenu)
        {
            ItemsMenu = SetUpListFromParent(ItemListParent);
            CurrentMenu = ItemsMenu;
        }
    }
    void SelectionMovement()
    {
        UiSelectionKnife.transform.position = CurrentMenu[posinlist].transform.position - knifeoffset;
        // close menu
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CurrentMenu = MainButtons;
            
            if (!ShowMenu)
            {
                SetChildrenActive(MenuChildren, true);
                ShowMenu = true;
                posinlist = 0;
                return;
            }
            SetChildrenActive(SetUpListFromParent(this.gameObject), false);
            SetChildrenActive(MenuChildren, false);
            FirstMenuParent.SetActive(true);
            ConfirmMenu.SetActive(false);
            ShowMenu = false;
        }
        if (!ShowMenu)
        {
            return;
        }
        // menu movement
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            posinlist = CurrentMenu.Count - 1;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            posinlist--;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            posinlist++;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
        {
            if (CurrentMenu[posinlist] != null) {
                CurrentMenu[posinlist].GetComponent<Button>().onClick.Invoke();
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            CurrentMenu = MainButtons;
        }
        try
        { UiSelectionKnife.transform.position = CurrentMenu[posinlist].transform.position - knifeoffset; }
        catch (ArgumentOutOfRangeException ex) { posinlist = 0; }
    }

    public void MoveToItemList()
    {
        if (ItemsMenu.Count == 0)
        {
            print("Theres no items");
            return;
        }
        ItemsMenu = SetUpListFromParent(ItemListParent);
        knifeoffset = new Vector3(330, -80, 0);
        posinlist = 0;
        CurrentMenu = ItemsMenu;
        if (ItemsMenu[posinlist] != null)
        {
            DisplayItems.DisplayObject(ItemsMenu[posinlist].GetComponent<ItemContainer>().ThisItems);
        }
    }
    public void MoveToConfirmList()
    {
        usedItemText = ItemsMenu[posinlist];
        ItemToUse = ItemsMenu[posinlist].GetComponent<ItemContainer>().ThisItems;
        knifeoffset = new Vector3(150, -4, 0);
        posinlist = 0;
        ConfirmMenuOptions = SetUpListFromParent(ConfirmMenu);
        CurrentMenu = ConfirmMenuOptions;
    }
    public void MoveToHeroSelection()
    {

        posinlist = 0;
        CurrentMenu = HeroStatsMenu;
    }
    public void UseItem()
    {
        PlayerToUse = HeroStatsMenu[posinlist].GetComponent<ShowingHeroStats>().HeroStats;
        knifeoffset = new Vector3(160, -170);
        if (ItemToUse != null) {
            UseItemOnPlayer(PlayerToUse, ItemToUse);
            print($"{ItemToUse.name} was consoumed");
            ItemsMenu.Remove(usedItemText);
            Destroy(usedItemText);
            DisplayItems.UseItem(ItemToUse);
            CurrentMenu = ItemsMenu;
        }
        ItemToUse = null;
        if (ItemsMenu.Count == 0)
        {
            SetChildrenActive(SetUpListFromParent(this.gameObject), false);
            SetChildrenActive(MenuChildren, false);
            FirstMenuParent.SetActive(true);
            ShowMenu = false;
            CurrentMenu = MainButtons;
            return;
        }
        MoveToItemList();
    }
    /// <summary>
    /// Stops the player form being able to move depending on the passed bool. False don't move, True move
    /// </summary>
    /// <param name="State"></param>
    void SetPlayerState(bool State)
    {
        InScenePlayerScript.enabled = State;
    }
    void SetChildrenActive(List<GameObject> Children, bool State)
    {
        foreach (GameObject _Var in Children)
        {
            _Var.SetActive(State);
        }
    }
    List<GameObject> SetUpListFromParent(GameObject Parent)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < Parent.transform.childCount; i++)
        {
            list.Add(Parent.transform.GetChild(i).gameObject);
        }
        return list;
    }
    void UseItemOnPlayer(PlayerStats PS, ItemsObjects IO)
    {
        PS.CurrentHealth += IO.HpChange;
        PS.Speedstat += IO.SpeedChange;
        PS.Attackstat += IO.AttackChange;
        PS.Defensestat += IO.DefenseChange;
    }
}
