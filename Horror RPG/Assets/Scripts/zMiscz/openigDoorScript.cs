using UnityEngine;

public class openigDoorScript : MonoBehaviour, IInteractable
{
    [Tooltip("Use attached sceneChangeScript for going to the next level")]
    public bool useSceneChangeScript;
    public bool returnToLastScene;
    public float distanceToOpen;
    public Sprite openedDoorSprite;
    public EventCore EventCore { get; set;}
    public bool toggleState { get; set;}


    IInteractable _IInteractable;
    private void Start()
    {
        _IInteractable = GetComponent<IInteractable>();
        _IInteractable.setupEventCore();
        Debug.Log(EventCore.gameObject.name);
        EventCore.EV_ObjectInteract.AddListener(StartInteraction);
    }
   
    void StartInteraction(Vector3 heroTransform)
    {
        Debug.Log("Interaction event fired");
        if (_IInteractable.IsThePlayerInteraction(transform.position, heroTransform))
        {
            Debug.Log("The player is moving to a diffrent scene");
            toggleOn();
        }
    }

    public void toggleOn()
    {
        if (returnToLastScene)
        {
            GetComponent<sceneChange>().loadScene("", GetComponent<sceneChange>()._lastSceneOccupied.indexOfLastScene);
        }

        if (useSceneChangeScript)
        {
            GetComponent<sceneChange>().loadScene();
        }
    }

    public void toggleOff()
    {

    }
   
}
