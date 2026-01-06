using UnityEngine;

[CreateAssetMenu(fileName = "EnemyObjects", menuName = "Scriptable Objects/EnemyObjects")]
public class EnemyObjects : ScriptableObject
{
    public string EnemyName;
    [Tooltip("Health Stat")]
    public float Healthstat = 5;
    [Tooltip("Attack Stat")]
    public float Attackstat = 5;
    [Tooltip("Defense Stat")]
    public float Defensestat = 5;
    [Tooltip("Speed Stat")]
    public float Speedstat = 5;
    [Tooltip("Sprite")]
    public Sprite Spriteimage;
    [Space(10)]
    [Tooltip("Rewarded for killing the enemy")]
    public int GoldToBeRewarded;
}
