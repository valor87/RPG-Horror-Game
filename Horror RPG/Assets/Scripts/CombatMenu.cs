using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class CombatMenu : MonoBehaviour
{
    public List<GameObject> EnemiesInScene;
    public List<GameObject> CombatActions;
    public List<GameObject> AttackActions;
    public List<List<GameObject>> MenuOptions = new List<List<GameObject>>();
    public List<GameObject> CurrentMenu;
    public List<GameObject> EnemyStatsUi;
    GameObject Highlight;
    GameObject KnifeInGameScene;
    Vector3 knifeoffset = new Vector3(175,0,0); // offset for the knife
    public int posinlist = 0;
    bool PickTargets;
    
    void Start()
    {
        // adding all lists to a main list
        MenuOptions.Add(CombatActions);
        MenuOptions.Add(AttackActions);
        MenuOptions.Add(EnemiesInScene);
        KnifeInGameScene = GameObject.Find("GameObjectKnife");
        Highlight = GameObject.Find("SlectionKnife");
        KnifeInGameScene.SetActive(false);
        CurrentMenu = CombatActions;
    }
    private void Awake()
    {
        for (int i = 0; i < EnemiesInScene.Count; i++)
        {
            GameObject ParentSlider = GameObject.Find("Enemy " + (i + 1));
            EnemyStatsUi.Add(ParentSlider);
            GameObject Hpslider = ParentSlider.transform.GetChild(0).gameObject;
            EnemiesInScene[i].GetComponent<EnemyStats>().HpSlider = Hpslider.GetComponent<Slider>();
        }
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
            CalculateDamage(CurrentMenu[posinlist], null);
            ResetMenue();
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
        if (!PickTargets) {
            foreach (GameObject f in CurrentMenu)
            {
                f.SetActive(false);
            }
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
        ChangeMenu(2);
    }

    public void CalculateDamage(GameObject Target, GameObject Attacker)
    {
        float EnemyHp = Target.GetComponent<EnemyStats>().Hp;
        EnemyHp -= 2.5f;
        Target.GetComponent<EnemyStats>().Hp = EnemyHp;
        if (EnemyHp <= 0) {
            print(EnemiesInScene.IndexOf(Target));
            GameObject UiStat = EnemyStatsUi[EnemiesInScene.IndexOf(Target)];
            EnemyStatsUi.Remove(UiStat);
            Destroy(UiStat);
            EnemiesInScene.Remove(Target);
            Destroy(Target);
        }
    }
    void ResetMenue()
    {
        if (CheckIfEnemiesAreDead(EnemiesInScene))
        {
            print("you win");
        }
        Highlight.GetComponent<Animator>().speed = 1;
        KnifeInGameScene.SetActive(false);
        ChangeMenu(0);
        PickTargets = false;
        posinlist = 0;
    }
    bool CheckIfEnemiesAreDead(List<GameObject> Enemies)
    {
        if (Enemies.Count != 0)
        {
            return false;
        }
        return true;
    }
}
