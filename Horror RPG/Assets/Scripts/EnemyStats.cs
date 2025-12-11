using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class EnemyStats : MonoBehaviour
{
    [Header("Setting Stats")]
    public GameObject PlayerStats;
    public float Hp = 5;
    public float Attack = 2;
    [SerializeField] float Defense = 2;
    public int speedStat = 2;
    public Slider HpSlider;

    [Space(10)]
    [Header("For Player Characters")]
    [HideInInspector]
    public List<GameObject> AttackButtons;
    [Tooltip("Is the script on a playable character // runs player actions")]
    public bool Isplayer;

    [Space(10)]
    [Header("for debuging")]
    public GameObject TargetEnemy;
    public List<CombatAttackActions> CAA;
    public string AttackAction;

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
   
    void Start()
    {
        
        Hp = Mathf.Clamp(Hp, 0, Hp);
        HpSlider.maxValue = Hp;
    }
    private void Update()
    {
        HpSlider.value = Hp;
    }
    private void SetPlayerStats()
    {
        
    }
    public void SetAttacksForPlayers(string WantedAttack)
    {
        foreach (CombatAttackActions _var in CAA)
        {
            if (_var.ActionName == WantedAttack)
            {
                print($"Setting player action looking for {WantedAttack}");
                Attack = Attack + _var.Damage;
                print($"Attack set to {Attack}");
            }
        }
    }
}
