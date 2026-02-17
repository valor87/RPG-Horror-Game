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
    public TextMeshProUGUI LevelValue;
    public Slider HpField;
    public Slider ExpSlider;
    public Image PlayerImage;
   
    private void SetValues()
    {
        NameTextField.text = HeroStats.HeroName;
        AttackValue.text = HeroStats.Attackstat.ToString();
        DefenseValue.text = HeroStats.Defensestat.ToString();
        SpeedValue.text = HeroStats.Speedstat.ToString();
        LevelValue.text = HeroStats.Level.ToString();
        HpField.maxValue = HeroStats.Healthstat;
        HpField.value = HeroStats.CurrentHealth;
        ExpSlider.value = HeroStats.CurrentEXP;
        PlayerImage.sprite = HeroStats.Spriteimage;
    }
    // Update is called once per frame
    void Update()
    {
        SetValues();
        HpField.value = HeroStats.CurrentHealth;
        ExpSlider.value = HeroStats.CurrentEXP;
    }
}
