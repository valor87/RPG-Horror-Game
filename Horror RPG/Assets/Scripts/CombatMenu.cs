using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
public class CombatMenu : MonoBehaviour
{
    [Header("Lists of Objects in the combat scene")]
    public List<GameObject> EnemiesInScene;
    public List<GameObject> CombatActions;
    public List<GameObject> AttackActions;
    List<List<GameObject>> MenuOptions = new List<List<GameObject>>();
    [Header("For Debuging")]
    public List<GameObject> CurrentMenu;
    public List<GameObject> EnemyStatsUi;
    public List<GameObject> inichative = new List<GameObject>();
    public int posinlist = 0;
    [Space(5)]
    [Header("For selecting menu options")]
    public GameObject UiSelectionKnife;
    public GameObject KnifeInGameScene;
    Vector3 knifeoffset = new Vector3(175, 0, 0); // offset for the knife in UI
    // for menu navagation
    bool PickTargets;
    bool CanSelectActions = true;
    void Start()
    {
        // adding all lists to a main list
        MenuOptions.Add(CombatActions);
        MenuOptions.Add(AttackActions);
        MenuOptions.Add(EnemiesInScene);

        KnifeInGameScene.SetActive(false);
        CurrentMenu = CombatActions;

        Inichative(null, EnemiesInScene);

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
        if (!CanSelectActions)
        {
            return;
        }
        SelectActions();
        SelectionMovement();

        if (!PickTargets)
        {
            SelectButton();
        }
        else
        {
            UiSelectionKnife.GetComponent<Animator>().speed = 0;
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
            posinlist = CurrentMenu.Count;
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
        if (posinlist >= 0 || posinlist <= CurrentMenu.Count)
        {
            UiSelectionKnife.transform.position = CurrentMenu[posinlist].transform.position - knifeoffset;
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
        KnifeInGameScene.transform.position = EnemiesInScene[posinlist].transform.position + new Vector3(-1.5f, 0, 0);
    }

    public void ChangeMenu(int menuNum)
    {
        posinlist = 0;

        if (menuNum < 0 || menuNum > MenuOptions.Count)
        {
            return;
        }
        if (!PickTargets)
        {
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
    void ResetMenue()
    {
        if (CheckIfEnemiesAreDead(EnemiesInScene))
        {
            print("you win");
        }

        UiSelectionKnife.GetComponent<Animator>().speed = 1;
        KnifeInGameScene.SetActive(false);
        PickTargets = false;
        posinlist = 0;
        CurrentMenu = MenuOptions[1];
        ChangeMenu(0);
    }
    public void GunAttack()
    {
        PickTargets = true;
        ChangeMenu(2);
    }
    private void Inichative(List<GameObject> players, List<GameObject> Enemies)
    {

        foreach (GameObject f in Enemies)
        {

            int currspeed = f.GetComponent<EnemyStats>().speedStat;
            

            for (int i = 0; i <= inichative.Count; i++)
            {
                print($"Loop has run {i} times with {f} with a {inichative.Count} total in inichative");

                if (Enemies[i].GetComponent<EnemyStats>().speedStat >= currspeed)
                {
                    inichative.Insert(i, f);
                    break;
                }
                if (i == inichative.Count)
                {
                    inichative.Add(f);
                    continue;
                }
                continue;

            }
        }
    }
    public void CalculateDamage(GameObject Target, GameObject Attacker)
    {
        StartCoroutine(DealDamageSlowly(Target, 2.5f));
    }

    bool CheckIfEnemiesAreDead(List<GameObject> Enemies)
    {
        if (Enemies.Count != 0)
        {
            return false;
        }
        return true;
    }

    IEnumerator DealDamageSlowly(GameObject RecevingDamage, float damage)
    {
        CanSelectActions = false;
        float decreaseHealth = 0.1f;
        while (0 <= damage)
        {
            print(damage);
            damage -= decreaseHealth;
            RecevingDamage.GetComponent<EnemyStats>().Hp -= decreaseHealth;
            yield return new WaitForSeconds(.01f);
        }
        float EnemyHp = RecevingDamage.GetComponent<EnemyStats>().Hp;

        if (EnemyHp <= 0)
        {
            yield return new WaitForSeconds(1);
            GameObject UiStat = EnemyStatsUi[EnemiesInScene.IndexOf(RecevingDamage)];
            EnemyStatsUi.Remove(UiStat);
            EnemiesInScene.Remove(RecevingDamage);
            Destroy(UiStat);
            Destroy(RecevingDamage);
        }
        CanSelectActions = true;
    }
}
