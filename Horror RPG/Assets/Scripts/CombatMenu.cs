using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
public class CombatMenu : MonoBehaviour
{
    [Header("Lists of Objects in the combat scene")]
    [Tooltip("Parent that holds children of all enimies in the scene")]
    public GameObject EnemyParent;
    [Tooltip("Parent that holds children of all heros characters in the scene")]
    public GameObject HerosCharactersParent;
    [Tooltip("Parent that holds all combat actions in the scene")]
    public GameObject CombatActionsParent;
    [Tooltip("Parent that holds Attack options in the scene || include a back option")]
    public GameObject AttackActionsParent;
    [Tooltip("Parent that holds children of all the Enemy Ui health sliders")]
    public GameObject UiCreatureParents;
    [Tooltip("Parent that holds children of all the Hero Ui health sliders")]
    public GameObject UiHeroParents;

    List<GameObject> EnemiesInScene = new List<GameObject>();
    public List<GameObject> HerosInScene = new List<GameObject>();
    List<GameObject> CombatActions = new List<GameObject>();
    List<GameObject> AttackActions = new List<GameObject>();
    List<List<GameObject>> MenuOptions = new List<List<GameObject>>();

    [Header("For Debuging")]
    public List<GameObject> CurrentMenu;
    public List<GameObject> EnemyStatsUi;
    public List<GameObject> HeroStatsUi;
    public List<GameObject> inichative = new List<GameObject>();
    public int posinlist = 0;

    [Space(5)]
    [Header("For selecting menu options")]
    public GameObject UiSelectionKnife;
    public GameObject KnifeInGameScene;
    Vector3 knifeoffset = new Vector3(175, 0, 0); // offset for the knife in UI

    // Selecting Enemy
    GameObject Target;
    bool PlayerSelectingActions;
    float DamageFromHero;
    // for menu navagation
    bool PickTargets;
    bool CanSelectActions = true;
    bool playerselectingActions;
    // Setup
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void Awake()
    {
        SetupLists(EnemyParent, EnemiesInScene);
        SetupLists(CombatActionsParent, CombatActions);
        SetupLists(AttackActionsParent, AttackActions);
        SetupLists(HerosCharactersParent, HerosInScene);
        
        // adding all lists to a main list
        MenuOptions.Add(CombatActions);
        MenuOptions.Add(AttackActions);
        MenuOptions.Add(EnemiesInScene);
        for (int i = 0; i < EnemiesInScene.Count; i++)
        {
            GameObject ParentSlider = UiCreatureParents.transform.GetChild(i).gameObject;
            EnemyStatsUi.Add(ParentSlider);
            GameObject Hpslider = ParentSlider.transform.GetChild(0).gameObject;
            EnemiesInScene[i].GetComponent<EnemyStats>().HpSlider = Hpslider.GetComponent<Slider>();
        }
        for (int i = 0; i < HerosInScene.Count; i++)
        {
            GameObject ParentSlider = UiHeroParents.transform.GetChild(i).gameObject;
            HeroStatsUi.Add(ParentSlider);
            GameObject Hpslider = ParentSlider.transform.GetChild(0).gameObject;
            HerosInScene[i].GetComponent<EnemyStats>().HpSlider = Hpslider.GetComponent<Slider>();
        }
    }

    void Start()
    {
        KnifeInGameScene.SetActive(false);
        CurrentMenu = CombatActions;
        Inichative(HerosInScene, EnemiesInScene);
        GetAllHeroActions(HerosInScene);
    }

    void SetupLists(GameObject _ParentofListElements, List<GameObject> _ChildInList)
    {

        foreach (Transform child in _ParentofListElements.transform)
        {
            _ChildInList.Add(child.gameObject);

            if (_ParentofListElements == HerosCharactersParent)
            {
                print($"Running for {child.name}");
                child.gameObject.GetComponent<EnemyStats>().AttackButtons = AttackActions;
                //child.gameObject.GetComponent<EnemyStats>().AttackButtons.RemoveAt(3);
            }
        }

    }
    // end of setup
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void Update()
    {
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
    void SelectTarget(GameObject Attacker)
    {
        CurrentMenu = EnemiesInScene;
        if (!KnifeInGameScene.activeInHierarchy)
        {
            posinlist = 0;
            KnifeInGameScene.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Target = CurrentMenu[posinlist];
            CanSelectActions = true;
            playerselectingActions = false;
            ResetMenue();
        }
        KnifeInGameScene.transform.position = EnemiesInScene[posinlist].transform.position + new Vector3(-1.5f, 0, 0);
    }
    private void RunAttackSequence(List<GameObject> Inichative)
    {
        float damage = 5;
        StartCoroutine(DealDamageSlowly(Inichative, damage));
    }
    void GetAllHeroActions(List<GameObject> CurrentHeros)
    {
        StartCoroutine(PlayerPickOptions(CurrentHeros));
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
    public void ButtonAttack()
    {
        PickTargets = true;
        ChangeMenu(2);
    }
    private void StoreActions(GameObject Attacker, GameObject Target, string AttackName)
    {
        EnemyStats ES = Attacker.GetComponent<EnemyStats>();
        ES.TargetEnemy = Target;
        ES.AttackAction = AttackName;
    }
    private void Inichative(List<GameObject> players, List<GameObject> Enemies)
    {
        foreach (GameObject f in Enemies)
        {
            int currspeed = f.GetComponent<EnemyStats>().speedStat;

            for (int i = 0; i <= inichative.Count; i++)
            {
                if (i == inichative.Count)
                {
                    inichative.Add(f);
                    break;
                }
                if (inichative[i].GetComponent<EnemyStats>().speedStat >= currspeed)
                {
                    inichative.Insert(i, f);
                    break;
                }

                continue;

            }
        }
        foreach (GameObject f in players)
        {

            int currspeed = f.GetComponent<EnemyStats>().speedStat;

            for (int i = 0; i <= inichative.Count; i++)
            {
                if (i == inichative.Count)
                {
                    inichative.Add(f);
                    break;
                }
                if (inichative[i].GetComponent<EnemyStats>().speedStat >= currspeed)
                {
                    inichative.Insert(i, f);
                    break;
                }

                continue;

            }
        }
    }
    public void CalculateDamage(GameObject Target, float AttackerDamage)
    {
        float playerDamage = AttackerDamage;
        //StartCoroutine(DealDamageSlowly(Target, playerDamage));
    }

    bool CheckIfEnemiesAreDead(List<GameObject> Enemies)
    {
        if (Enemies.Count != 0)
        {
            return false;
        }
        return true;
    }

    IEnumerator DealDamageSlowly(List<GameObject> _Inichative, float incomingdamage)
    {
        for (int i = 0; i < _Inichative.Count; i++)
        {
            float damage = incomingdamage;
            GameObject Attacker = _Inichative[i];
            GameObject RecevingDamage = _Inichative[i].GetComponent<EnemyStats>().TargetEnemy;
            CanSelectActions = false;
            Vector3 AttackingPlacement = Vector3.zero;
            print($"Attacking with {Attacker} hitting {RecevingDamage}");

            // make the attacker walk forward
            if (RecevingDamage == null)
            {
                continue;
            }
            if (Attacker.tag == "Hero")
            {
                AttackingPlacement += Vector3.right * 2;
            }
            else
            {
                AttackingPlacement += Vector3.left * 2;
            }
            Attacker.transform.position += AttackingPlacement;
            while (0 < damage)
            {
                float decreaseHealth = 0.1f;

                damage -= decreaseHealth;
                RecevingDamage.GetComponent<EnemyStats>().Hp -= decreaseHealth;
                yield return new WaitForSeconds(.01f);
            }

            float EnemyHp = RecevingDamage.GetComponent<EnemyStats>().Hp;

            if (EnemyHp <= 0 && RecevingDamage != null)
            {
                yield return new WaitForSeconds(1);
                if (Attacker.tag == "Enemy")
                {
                    GameObject UiStat = HeroStatsUi[HerosInScene.IndexOf(RecevingDamage)];
                    HeroStatsUi.Remove(UiStat);
                    HerosInScene.Remove(RecevingDamage);

                    inichative.Remove(RecevingDamage);
                    Destroy(UiStat);
                    Destroy(RecevingDamage);
                }
                else
                {
                    GameObject UiStat = EnemyStatsUi[EnemiesInScene.IndexOf(RecevingDamage)];
                    EnemyStatsUi.Remove(UiStat);
                    EnemiesInScene.Remove(RecevingDamage);
                    HerosInScene.Remove(RecevingDamage);

                    inichative.Remove(RecevingDamage);
                    Destroy(UiStat);
                    Destroy(RecevingDamage);
                    

                }
            }
            // send the attacker back
            Attacker.transform.position -= AttackingPlacement;
        }
        CanSelectActions = true;
        playerselectingActions = false;
        GetAllHeroActions(HerosInScene);

        if (CheckIfEnemiesAreDead(EnemiesInScene))
        {
            print("you win");
        }
    }
    private void EnemyPickActionOptions(List<GameObject> Enemies, List<GameObject> Targets)
    {
        foreach (GameObject t in Enemies)
        {
            int RandomTarget = UnityEngine.Random.Range(0, Targets.Count);
            t.GetComponent<EnemyStats>().TargetEnemy = Targets[RandomTarget];
        }
    }
    IEnumerator PlayerPickOptions(List<GameObject> currentHero)
    {
        PlayerSelectingActions = true;
        foreach (GameObject Hero in currentHero)
        {
           
            print(AttackActions.Count);
            string PlayerDesiredAction = "";
            List<string> PlayerActionName = new List<string>();
            Hero.GetComponent<EnemyStats>().SetButtonActions(AttackActions, PlayerActionName);
            
            Hero.transform.position += Vector3.right * 2;
            playerselectingActions = true;

            while (playerselectingActions)
            {
                SelectActions();
                SelectionMovement();

                if (!PickTargets)
                {
                    PlayerDesiredAction = AttackActions[posinlist].name;
                    SelectButton();
                }
                else
                {
                    UiSelectionKnife.GetComponent<Animator>().speed = 0;
                    SelectTarget(Hero);
                    GameObject TargetToHit = Target;
                    StoreActions(Hero, TargetToHit, PlayerDesiredAction);
                }
                yield return null;
            }

            Hero.transform.position += Vector3.left * 2;
        }
        PlayerSelectingActions = false;
        EnemyPickActionOptions(EnemiesInScene, HerosInScene);
        RunAttackSequence(inichative);
    }
}
