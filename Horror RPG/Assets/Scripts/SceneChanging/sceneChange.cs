using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{
    [Header("Scene Reference")]
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private int sceneIndex;
    [Tooltip("The position of the player when they ENTER this door")]
    [SerializeField]
    private SceneChangePosition thisScenePosition;
    [HideInInspector]
    public lastSceneOccupied _lastSceneOccupied = new lastSceneOccupied();

    private void Start()
    {
        Debug.Log("new Scene");
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        hero.transform.position = thisScenePosition.position;
    }

    public void loadScene(string _sceneName = "", int _sceneIndex = 0)
    {
        _lastSceneOccupied.indexOfLastScene = SceneManager.GetActiveScene().buildIndex;

        if (_sceneName != "")
        {
            SceneManager.LoadScene(sceneName);
        }

        if (_sceneIndex != 0)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        if (sceneName != "")
        {
            SceneManager.LoadScene(sceneName);
        }

        if (sceneIndex != 0)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
   }
