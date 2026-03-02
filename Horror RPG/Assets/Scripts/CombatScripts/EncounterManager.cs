using UnityEngine;
using System.Collections.Generic;
public class EncounterManager : MonoBehaviour
{
    
    public static EncounterManager instance;
    public List<EnemyObjects> PotencialEnemy = new List<EnemyObjects>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
