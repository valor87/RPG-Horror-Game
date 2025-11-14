using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public float Hp = 5;
    [SerializeField] float Attack = 2;
    [SerializeField] float Defense = 2;
    public int speedStat = 2;
    public Slider HpSlider;
    float SliderHp;
    void Start()
    {
        Hp = Mathf.Clamp(Hp, 0, Hp);
        HpSlider.maxValue = Hp;
        
    }

    public void Update()
    {
        HpSlider.value = Hp;
    }

    public void AttackPlayers()
    {

    }
}
