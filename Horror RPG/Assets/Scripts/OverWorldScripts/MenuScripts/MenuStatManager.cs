using UnityEngine;
using System.Collections.Generic;
public class MenuStatManager : MonoBehaviour
{
    public List<GameObject> MenuPositions;
    public CurrentHerosInParty HerosInParty;
    public GameObject StatImagePrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < HerosInParty.HerosInScene.Count; i++)
        {
            MenuPositions[i].SetActive(true);
            MenuPositions[i].GetComponent<ShowingHeroStats>().HeroStats = HerosInParty.HerosInScene[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
