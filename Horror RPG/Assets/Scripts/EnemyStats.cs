using Unity.VisualScripting;
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
    public GameObject TargetEnemy;
    public bool Isplayer;

    [System.Serializable]

    public struct MyData
    {
        public string Name;
        public float DamageValue;
    }
    public MyData[] PlayerAttack;

    void Start()
    {
        Hp = Mathf.Clamp(Hp, 0, Hp);
        HpSlider.maxValue = Hp;
    }

    public void Update()
    {
        HpSlider.value = Hp;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerAttack[0].DamageValue = 4;
        }
    }

    public void AttackPlayers()
    {

    }
}
