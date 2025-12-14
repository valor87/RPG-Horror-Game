using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class EncounterManager : MonoBehaviour
{
    public List<EnemyObjects> PotencialEnemy = new List<EnemyObjects>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
