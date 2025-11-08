using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CombatMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<GameObject> EnemiesInScene;
    public List<GameObject> CombatActions;
    public List<GameObject> AttackActions;
    public List<GameObject> CurrentMenu;
    GameObject Highlight;
    GameObject KnifeInGameScene;
    Vector3 knifeoffset = new Vector3(175,0,0); // offset for the knife
    public int posinlist = 0;
    bool PickTargets;
    void Start()
    {
        KnifeInGameScene = GameObject.Find("GameObjectKnife");
        Highlight = GameObject.Find("SlectionKnife");
        KnifeInGameScene.SetActive(false);
        CurrentMenu = CombatActions;
    }

    // Update is called once per frame
    void Update()
    {
        SelectActions();
        SelectionMovement();

        if (!PickTargets)
        {
            SelectButton();
        }
        else
        {
            SelectTarget();
            Highlight.GetComponent<Animator>().speed = 0;
        }

    }
    void SelectionMovement()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            posinlist = CurrentMenu.Count - 1;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            posinlist--;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            posinlist++;
        }
    }
    void SelectActions()
    {
        
        if (posinlist < 0)
        {
            posinlist  = CurrentMenu.Count;
        }
        if (posinlist >= CurrentMenu.Count)
        {
            posinlist = 0;
        }


    }
    void SelectButton()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurrentMenu[posinlist].GetComponent<Button>().onClick.Invoke();
        }
        if (posinlist >= 0 || posinlist <= CurrentMenu.Count) {
            Highlight.transform.position = CurrentMenu[posinlist].transform.position - knifeoffset;
        }
    }
    void SelectTarget()
    {
        CurrentMenu = EnemiesInScene;
        if (!KnifeInGameScene.activeInHierarchy)
        {
            KnifeInGameScene.SetActive(true);
        }

        KnifeInGameScene.transform.position = EnemiesInScene[posinlist].transform.position + new Vector3 (-1.5f, 0, 0);
    }
    public void ToAttackMenu()
    {
        posinlist = 0;

        foreach (GameObject f in CurrentMenu)
        {
            f.SetActive(false);
        }

        CurrentMenu = AttackActions;

        foreach (GameObject f in CurrentMenu)
        {
            f.SetActive(true);
        }
    }
    public void BackToMainCombat()
    {
        posinlist = 0;

        foreach (GameObject f in CurrentMenu)
        {
            f.SetActive(false);
        }

        CurrentMenu = CombatActions;

        foreach (GameObject f in CurrentMenu)
        {
            f.SetActive(true);
        }
    }

    public void GunAttack()
    {
        posinlist = 0;
        PickTargets = true;
    }
}
