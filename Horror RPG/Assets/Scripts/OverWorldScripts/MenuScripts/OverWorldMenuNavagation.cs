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
    // Buttons list
    public List<GameObject> MainButtons = new List<GameObject>();

    // misc menu refs
    List<GameObject> MenuChildren = new List<GameObject>();
    List<GameObject> CurrentMenu = new List<GameObject>();

    // menu navagation
    bool ShowMenu;
    int posinlist;
    [Space(5)]
    [Header("For selecting menu options")]
    public GameObject UiSelectionKnife;
    Vector3 knifeoffset = new Vector3(55, 0, 0); // offset for the knife in UI
    void Start()
    {
        MenuChildren = SetUpListFromParent(FirstMenuParent.gameObject);
        MainButtons = SetUpListFromParent(ButtonsParent);
        CurrentMenu = MainButtons;
    }

    // Update is called once per frame
    void Update()
    {
        SelectionMovement();
        SetPlayerState(!ShowMenu);
        SelectButton();
    }
    void SelectionMovement()
    {
        UiSelectionKnife.transform.position = CurrentMenu[posinlist].transform.position - knifeoffset;
        // close menu
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!ShowMenu)
            {
                SetChildrenActive(MenuChildren, true);
                ShowMenu = true;
                return;
            }
            SetChildrenActive(SetUpListFromParent(this.gameObject), false);
            SetChildrenActive(MenuChildren, false);
            FirstMenuParent.SetActive(true);
            ShowMenu = false;
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
        try
        { UiSelectionKnife.transform.position = CurrentMenu[posinlist].transform.position - knifeoffset; }
        catch (ArgumentOutOfRangeException ex) { posinlist = 0; }
    }
    void SelectButton()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurrentMenu[posinlist].GetComponent<Button>().onClick.Invoke();
        }

    }
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
}
