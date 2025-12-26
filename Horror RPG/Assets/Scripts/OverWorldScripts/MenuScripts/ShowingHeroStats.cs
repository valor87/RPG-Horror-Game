using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowingHeroStats : MonoBehaviour
{
    public PlayerStats HeroStats;

    [Header("Text for stats")]
    public TextMeshProUGUI NameTextField;
    public TextMeshProUGUI AttackValue;
    public TextMeshProUGUI SpeedValue;
    public TextMeshProUGUI DefenseValue;
    public Slider HpField;
    public Image PlayerImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NameTextField.text = HeroStats.HeroName;
        AttackValue.text = HeroStats.Attackstat.ToString();
        DefenseValue.text = HeroStats.Defensestat.ToString();
        SpeedValue.text = HeroStats.Speedstat.ToString();
        HpField.maxValue = HeroStats.Healthstat;
        HpField.value = HeroStats.CurrentHealth;
        PlayerImage.sprite = HeroStats.Spriteimage;
    }

    // Update is called once per frame
    void Update()
    {
        HpField.value = HeroStats.CurrentHealth;
    }
}
