using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CombatMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<GameObject> CombatActions;
    public List<GameObject> AttackActions;
    Vector3 knifeoffset = new Vector3(175,0,0);
    GameObject Highlight;
    GameObject ButtonHighlig;
    public List<GameObject> CurrentMenu;
    int pos = 0;

    void Start()
    {
        CurrentMenu = CombatActions;
        Highlight = GameObject.Find("SlectionKnife");
    }

    // Update is called once per frame
    void Update()
    {
        SelectActions(CurrentMenu);
    }

    void SelectActions(List<GameObject> CurrentMenu)
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            pos = CurrentMenu.Count-1;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pos--;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            pos++;
        }
        if (pos <= 0)
        {
            pos = CurrentMenu.Count;
        }
        if (pos >= CurrentMenu.Count)
        {
            pos = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurrentMenu[pos].GetComponent<Button>().onClick.Invoke();
        }

        Highlight.transform.position = CurrentMenu[pos].transform.position - knifeoffset;
    }
    public void ToAttackMenu()
    {
        CurrentMenu = AttackActions;
    }
    public void BackToMainCombat()
    {
        CurrentMenu = CombatActions;
    }
}
