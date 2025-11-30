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
    public Slider HpSlider;

    [Space(10)]
    [Header("For Player Characters")]
    public List<GameObject> AttackButtons;
    public GameObject TargetEnemy;
    public bool Isplayer;

    [System.Serializable]

    public struct MyData
    {
        public string Name;
        public float DamageValue;
    }
    public MyData[] PlayerAttack;

    public List<GameObject> SetButtonActions(List<GameObject> HerosAttacks)
    {

        for (int i = 0; i < 3; i++)
        {
            HerosAttacks[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = PlayerAttack[i].Name;

            HerosAttacks[i].GetComponent<Button>().name = PlayerAttack[i].DamageValue.ToString();
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
