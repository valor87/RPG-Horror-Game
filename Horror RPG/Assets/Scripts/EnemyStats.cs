using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class EnemyStats : MonoBehaviour
{
    [Header("Setting Stats")]
    public float Hp = 5;
    public float Attack = 2;
    [SerializeField] float Defense = 2;
    public int speedStat = 2;
    [HideInInspector]
    public Slider HpSlider;

    [Space(10)]
    [Header("For Player Characters")]
    [HideInInspector]
    public List<GameObject> AttackButtons;
    [Tooltip("Is the script on a playable chracter // runs player actions")]
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
            HerosAttacks[i].transform.GetChild(0).name = CAA[i].ActionName;
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
    public void AttackPlayers()
    {

    }
}
