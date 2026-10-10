using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{
    [Header("Scene Reference")]
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private int sceneIndex;
    [HideInInspector]
    public lastSceneOccupied _lastSceneOccupied = new lastSceneOccupied();

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
