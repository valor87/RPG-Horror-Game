using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class EnemyStats : MonoBehaviour
{
    [Header("Setting Stats")]
    public PlayerStats PlayerStats;
    public float Hp = 5;
    public float CurrentHealth;
    public float Attack = 2;
    public float Defense = 2;
    public float speedStat = 2;
    public Slider HpSlider;

    // for players only
    [Space(10)]
    [Header("For Player Characters")]
    [HideInInspector]
    public List<GameObject> AttackButtons;
    [Tooltip("Is the script on a playable character // runs player actions")]
    public bool Isplayer;

    //for enemys only
    public int GoldForPlayer;
    // for debuging
    [Space(10)]
    [Header("for debuging")]
    public GameObject TargetEnemy;
    public List<CombatAttackActions> CAA;
    public string AttackAction;
    public bool RunningAway;
    public List<GameObject> SetButtonActions(List<GameObject> HerosAttacks, List<string> ActionNames)
    {
        for (int i = 0; i < 3; i++)
        {
            HerosAttacks[i].GetComponent<Button>().name = CAA[i].ActionName;
            HerosAttacks[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = CAA[i].ActionName;
            ActionNames.Add(CAA[i].ActionName);
        }
        return HerosAttacks;
    }

    void SetupPlayerStats(PlayerStats PS)
    {
        Hp = PS.Healthstat;
        CurrentHealth = PS.CurrentHealth;
        Attack = PS.Attackstat;
        Defense = PS.Defensestat;
        speedStat = PS.Speedstat;
        this.gameObject.name = PS.HeroName;
        GetComponent<SpriteRenderer>().sprite = PS.Spriteimage;
    }
    public void SetupEnemyStats(EnemyObjects EO)
    {
        Hp = EO.Healthstat;
        Attack = EO.Attackstat;
        Defense = EO.Defensestat;
        speedStat = EO.Speedstat;
        GoldForPlayer = EO.GoldToBeRewarded;
        this.gameObject.name = EO.EnemyName;
        GetComponent<SpriteRenderer>().sprite = EO.Spriteimage;
    }
    void Start()
    {
        if (Isplayer)
        {
            SetupPlayerStats(PlayerStats);
        }
        else
        {
            CurrentHealth = Hp;
        }

        Hp = Mathf.Clamp(Hp, 0, Hp);
        HpSlider.maxValue = Hp;
    }
    private void Update()
    {
        HpSlider.value = CurrentHealth;
        if (this.gameObject.CompareTag("Hero")) {
            RunningAway = PlayerStats.WantsToRun;
        }
    }

    public void SetAttacksForPlayers(string WantedAttack)
    {
        foreach (CombatAttackActions _var in CAA)
        {
            if (_var.ActionName == WantedAttack)
            {
                Attack = Attack + _var.Damage;
            }
        }
    }
    private void OnDisable()
    {

        if (Isplayer)
        {
            PlayerStats.CurrentHealth = CurrentHealth;
        }
    }
}
