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
    [Tooltip("To mesure the players current exp")]
    public float CurrentEXP = 0;
    [Tooltip("Hero's Current Level")]
    public int Level = 1;
    [Tooltip("Sprite")]
    public Sprite Spriteimage;
    [Header("Level up values for the player")]
    [Tooltip("The amount of exsperirance for each level")]
    [SerializeField]
    int[] levelUpValues = new int[] {30,60,140,270,450,680,960,1290,1670,2100,2580};
    [Header("To be Changed during run time")]
    public bool WantsToRun;
    [ContextMenu("Level up character")]
    void processLevelUp()
    {
        float[] tempStatArray = new float[] {Healthstat,Attackstat,Defensestat,Speedstat};
        int[] statChangeAmount = new int[] { 1, 2, 2, 3 };
        for (int i = 0; i < tempStatArray.Length; i++)
        {
            tempStatArray[i] += statChangeAmount[Random.Range(0, statChangeAmount.Length)];
            Debug.Log(tempStatArray[i]);
        }
        Healthstat = tempStatArray[0];
        Attackstat = tempStatArray[1];
        Defensestat = tempStatArray[2];
        Speedstat = tempStatArray[3];
    }

    
    private void OnEnable()
    {
        WantsToRun = false;
    }
}
