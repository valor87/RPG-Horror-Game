using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CombatMenu : MonoBehaviour
{
    public List<List<GameObject>> MenuOptions = new List<List<GameObject>>();
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
        MenuOptions.Add(CombatActions);
        MenuOptions.Add(AttackActions);
        MenuOptions.Add(EnemiesInScene);
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
        print(posinlist);
        if (!PickTargets)
        {
            SelectButton();
        }
        else
        {
            Highlight.GetComponent<Animator>().speed = 0;
            SelectTarget();
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
            posinlist = 0;
            KnifeInGameScene.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetMenue();
            //CalculateDamage();
        }
        KnifeInGameScene.transform.position = EnemiesInScene[posinlist].transform.position + new Vector3 (-1.5f, 0, 0);

    }
 
    public void ChangeMenu(int menuNum)
    { 
        posinlist = 0;
        if (menuNum < 0 || menuNum > MenuOptions.Count)
        {
            return;
        }

        foreach (GameObject f in CurrentMenu)
        {
            f.SetActive(false);
        }

        CurrentMenu = MenuOptions[menuNum];

        foreach (GameObject f in CurrentMenu)
        {
            f.SetActive(true);
        }
    }

    public void GunAttack()
    {
        PickTargets = true;
    }

    public void CalculateDamage(GameObject Target, GameObject Attacker)
    {

    }
    void ResetMenue()
    {
        Highlight.GetComponent<Animator>().speed = 1;
        PickTargets = false;
        KnifeInGameScene.SetActive(false);
        CurrentMenu = AttackActions;
        //BackToMainCombat();
        posinlist = 0;
    }
}
