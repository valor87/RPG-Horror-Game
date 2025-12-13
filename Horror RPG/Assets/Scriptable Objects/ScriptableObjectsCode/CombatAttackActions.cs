using UnityEngine;

[CreateAssetMenu(fileName = "CombatAttackActions", menuName = "Scriptable Objects/CombatAttackActions")]
public class CombatAttackActions : ScriptableObject
{
    [Tooltip("Name of Action")]
    public string ActionName = "";

    [Tooltip("Action Effect")]
    public bool Attack = true;
    [Tooltip("Action Effect")]
    public bool Support;

    [Tooltip("Damage of Attack")]
    [Range(1, 20)]
    public float Damage = 0;

    [Header("Support Effect")]
    public bool Healing = false;
    public float HealAmount = 0;
}
