using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetSceneManager : MonoBehaviour
{
    List<AsyncOperation> ScenesToLoad = new List<AsyncOperation>();
    Scene OverWorld;
    Scene CombatScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CombatScene = SceneManager.GetSceneAt(0);
        print($"{CombatScene.name}");
        OverWorld = SceneManager.GetActiveScene();
        print($"{OverWorld.name}");
        SceneManager.LoadSceneAsync("CombatScene", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.UnloadSceneAsync("CombatScene");
        }
    }
}
