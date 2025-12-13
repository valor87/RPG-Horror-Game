using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]


public class PlayerStats : ScriptableObject
{
    [Tooltip("Name of Hero")]
    public string HeroName = "";
    [Tooltip("Health Stat")]
    public float Healthstat = 5;
    [Tooltip("Current Health Stat")]
    public float CurrentHealth = 5;
    [Tooltip("Attack Stat")]
    public float Attackstat = 5;
    [Tooltip("Defense Stat")]
    public float Defensestat = 5;
    [Tooltip("Speed Stat")]
    public float Speedstat = 5;
    [Tooltip("Sprite")]
    public Sprite Spriteimage;
    private void OnEnable()
    {
        //CurrentHealth = Healthstat;
    }
}
