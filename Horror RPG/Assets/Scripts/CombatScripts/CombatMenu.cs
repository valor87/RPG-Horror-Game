using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [Header("PreFab of the player to be changed by script")]
    public GameObject PlayerPreFab;
    [Header("PreFab of the enemy to be changed by script")]
    public GameObject EnemyPreFab;
    [Header("For changing players values and gold")]
    public PlayerValues PV;
    List<GameObject> EnemiesInScene = new List<GameObject>();
    [Space(10)]
    public List<GameObject> HerosInScene = new List<GameObject>();
    List<GameObject> CombatActions = new List<GameObject>();
    List<GameObject> AttackActions = new List<GameObject>();
    List<List<GameObject>> MenuOptions = new List<List<GameObject>>();

    [Header("For Debuging")]
    public GameObject enemyencounterlist;
    public List<GameObject> CurrentMenu;
    public List<GameObject> EnemyStatsUi;
    public List<GameObject> HeroStatsUi;
    public List<GameObject> inichative = new List<GameObject>();
    public int posinlist = 0;
    GameObject GameManager;
    [Space(5)]
    [Header("For selecting menu options")]
    public GameObject UiSelectionKnife;
    public GameObject KnifeInGameScene;
    Vector3 knifeoffset = new Vector3(55, 0, 0); // offset for the knife in UI
    bool PlayerRunAction;
    // Selecting Enemy
    GameObject Target;
    bool PlayerSelectingActions;
    float DamageFromHero;
    int GoldForThePlayer;
    // for menu navagation
    bool PickTargets;
    bool CanSelectActions = true;
    bool playerselectingActions;
    // Setup
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void setUpEnemyFromEncounterList()
    {
        enemyencounterlist = GameObject.Find("DontDestroyGameManager");
        List<EnemyObjects> Encounters = enemyencounterlist.GetComponent<EncounterManager>().PotencialEnemy;
        int AmountofEnemies = UnityEngine.Random.RandomRange(1, Encounters.Count + 1);
        for (int i = 0; i < AmountofEnemies; i++)
        {
            UiCreatureParents.transform.GetChild(i).gameObject.SetActive(true);
            Vector2 enemypos = new Vector2(5.8f, 3.6f);
            enemypos.y = enemypos.y - (2 * i);
            int RandomEnemy = UnityEngine.Random.RandomRange(0, Encounters.Count);
            GameObject CurrentEnemy = Instantiate(EnemyPreFab, enemypos, Quaternion.identity,EnemyParent.transform);
            CurrentEnemy.GetComponent<EnemyStats>().SetupEnemyStats(Encounters[RandomEnemy]);
            print($"wanting {AmountofEnemies} Running {i} times");
        }

    }
    private void setUpPlayersFromEncounterList()
    {
        enemyencounterlist = GameObject.Find("DontDestroyGameManager");
        List<PlayerStats> Players = enemyencounterlist.GetComponent<CurrentHerosInParty>().HerosInScene;
        int AmountofEnemies = enemyencounterlist.GetComponent<CurrentHerosInParty>().HerosInScene.Count;
        for (int i = 0; i < AmountofEnemies; i++)
        {
            UiHeroParents.transform.GetChild(i).gameObject.SetActive(true);
            Vector2 enemypos = new Vector2(-5.8f, 4);
            enemypos.y = enemypos.y - (2 * i);
            GameObject CurrentEnemy = Instantiate(PlayerPreFab, enemypos, Quaternion.identity, HerosCharactersParent.transform);
            CurrentEnemy.GetComponent<EnemyStats>().PlayerStats = enemyencounterlist.GetComponent<CurrentHerosInParty>().HerosInScene[i];
            print($"wanting {AmountofEnemies} Running {i} times");
        }

    }
    private void Awake()
    {
        setUpPlayersFromEncounterList();
        setUpEnemyFromEncounterList();

        // setting lists based on the parents
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
                child.gameObject.GetComponent<EnemyStats>().AttackButtons = AttackActions;
            }
        }

    }
    // end of setup
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void SelectionMovement()
    {
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
    }
    void SelectButton()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurrentMenu[posinlist].GetComponent<Button>().onClick.Invoke();
        }

        try
        { UiSelectionKnife.transform.position = CurrentMenu[posinlist].transform.position - knifeoffset; }
        catch (ArgumentOutOfRangeException ex) { posinlist = 0; }

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
        try { KnifeInGameScene.transform.position = EnemiesInScene[posinlist].transform.position + new Vector3(-1.5f, 0, 0); }
        catch (ArgumentOutOfRangeException ex)
        { posinlist = 0; }

    }
    private void RunAttackSequence(List<GameObject> Inichative)
    {
        StartCoroutine(DealDamageSlowly(Inichative));
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
            SceneManager.LoadScene(1);
        }

        UiSelectionKnife.GetComponent<Animator>().speed = 1;
        KnifeInGameScene.SetActive(false);
        PickTargets = false;
        posinlist = 0;
        CurrentMenu = MenuOptions[1];
        // set menu to the first selection
        ChangeMenu(0);
    }
    public void ButtonAttack()
    {
        PickTargets = true;
        // change the menu to the attack selection
        ChangeMenu(2);
    }
    public void ButtonRun()
    {
        /* Have the run button
         * use up the action of that player
         * and move onto the next hero
         */
        PlayerRunAction = true;
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
            float currspeed = f.GetComponent<EnemyStats>().speedStat;

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

            float currspeed = f.GetComponent<EnemyStats>().speedStat;

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
    }

    bool CheckIfEnemiesAreDead(List<GameObject> Enemies)
    {
        if (Enemies.Count != 0)
        {
            return false;
        }
        return true;
    }

    IEnumerator DealDamageSlowly(List<GameObject> _Inichative)
    {
        for (int i = 0; i < _Inichative.Count; i++)
        {
            GameObject Attacker = _Inichative[i];
            GameObject RecevingDamage = _Inichative[i].GetComponent<EnemyStats>().TargetEnemy;
            bool needBreak = false;
            bool WantsToRun = false;
            float incomingdamage = Attacker.GetComponent<EnemyStats>().Attack;
            float damage = incomingdamage;
            float EnemyHp = RecevingDamage.GetComponent<EnemyStats>().CurrentHealth;

            if (_Inichative[i].GetComponent<EnemyStats>().TargetEnemy == null)
            {
                print("its dead");
                continue;
            }
            
            CanSelectActions = false;
            Vector3 AttackingPlacement = Vector3.zero;

            if (Attacker.CompareTag("Hero"))
            {
                WantsToRun = Attacker.GetComponent<EnemyStats>().RunningAway;
                print($"stellar running is {WantsToRun}");
                AttackingPlacement += Vector3.right;
            }
            else
            {
                AttackingPlacement += Vector3.left * 1.5f;
            }
            Attacker.transform.position += AttackingPlacement;
            if (Attacker.CompareTag("Hero") && WantsToRun)
            {
                print("Trying to run");
                SceneManager.LoadScene(1);
            }
            while (0 < damage)
            {
                if (RecevingDamage == null)
                {
                    needBreak = true;
                    break;
                }
                float decreaseHealth = 0.1f;
                EnemyHp = RecevingDamage.GetComponent<EnemyStats>().CurrentHealth;
                damage -= decreaseHealth;
                RecevingDamage.GetComponent<EnemyStats>().CurrentHealth -= decreaseHealth;
                if (EnemyHp < 0)
                {
                    break;
                }
                yield return new WaitForSeconds(.01f);
            }
            if (needBreak)
            {
                Attacker.transform.position -= AttackingPlacement;
                continue;
            }

            EnemyHp = RecevingDamage.GetComponent<EnemyStats>().CurrentHealth;

            if (EnemyHp <= 0)
            {

                yield return new WaitForSeconds(1);
                if (RecevingDamage == null)
                {
                    continue;
                }
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
                    GoldForThePlayer += RecevingDamage.GetComponent<EnemyStats>().GoldForPlayer;
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
            SceneManager.LoadScene(1);
        }
    }
    private void EnemyPickActionOptions(List<GameObject> Enemies, List<GameObject> Targets)
    {
        if (Targets.Count == 0)
        {
            this.gameObject.SetActive(false);
            print("print the heros are dead");
            return;
        }
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
            string PlayerDesiredAction = "";
            List<string> PlayerActionName = new List<string>();
            Hero.GetComponent<EnemyStats>().SetButtonActions(AttackActions, PlayerActionName);
            PlayerRunAction = false;

            Hero.transform.position += Vector3.right * 1.5f;
            playerselectingActions = true;

            while (playerselectingActions)
            {
                
                SelectionMovement();
                if (!PickTargets)
                {
                    try { PlayerDesiredAction = AttackActions[posinlist].name; }
                    catch (ArgumentOutOfRangeException ex) { posinlist = 0; }
                    SelectButton();
                }
                else
                {
                    UiSelectionKnife.GetComponent<Animator>().speed = 0;
                    SelectTarget(Hero);
                    GameObject TargetToHit = Target;
                    StoreActions(Hero, TargetToHit, PlayerDesiredAction);
                }
                // have a condition for selecting the run action

                if (PlayerRunAction)
                {
                    // set running as the heros action in store action function
                    break;
                }
                yield return null;
            }
            if (!PlayerRunAction)
            {
                Hero.GetComponent<EnemyStats>().SetAttacksForPlayers(PlayerDesiredAction);
            }
            else if (PlayerRunAction)
            {
                Hero.GetComponent<EnemyStats>().PlayerStats.WantsToRun = true;
            }
            Hero.transform.position += Vector3.left * 2;
        }
        PlayerSelectingActions = false;
        EnemyPickActionOptions(EnemiesInScene, HerosInScene);
        RunAttackSequence(inichative);
    }
    private void OnDisable()
    {
        PV.Gold += GoldForThePlayer;
    }
}
