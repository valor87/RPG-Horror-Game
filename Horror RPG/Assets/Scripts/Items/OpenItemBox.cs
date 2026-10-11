using UnityEngine;
using UnityEngine.Rendering;

public class OpenItemBox : MonoBehaviour, IInteractable
{
    [Header("Item to give")]
    [SerializeField]
    private ItemsObjects itemToGive;
    [Space(10)]
    [Header("Interaction Properties")]
    [SerializeField]
    private Sprite boxSpriteChange;
    [SerializeField]
    private float distanceToOpen;

    public bool toggleState { get; set; }
    public EventCore EventCore { get; set; }
    public float interactionDistance { get; set; }
    
    IInteractable _Iinteractable;
    void Start()
    {
        toggleState = true;
        _Iinteractable = GetComponent<IInteractable>();
        _Iinteractable.setupEventCore();
        EventCore.EV_ObjectInteract.AddListener(checkPlayerInteraction);
        interactionDistance = distanceToOpen;
    }

    void checkPlayerInteraction(Vector3 heroPosition)
    {
        bool thisObject = _Iinteractable.IsThePlayerInteraction(transform.position, heroPosition);
        if (thisObject)
            toggleOn();
    }

    public void toggleOn()
    {
        if (!toggleState)
            return;

        EventCore.EV_ItemPickUp.Invoke(itemToGive);
        GetComponent<SpriteRenderer>().sprite = boxSpriteChange;
        displayText();
        toggleState = false;
    }

    private void displayText()
    {
        textDisplayManager textManager = ScriptableObject.CreateInstance<textDisplayManager>();
        textDialogue textDialogue = ScriptableObject.CreateInstance<textDialogue>();

        textDialogue[] texts = new textDialogue[1];
        
        textDialogue.Dialague = "You have gained a " + itemToGive.Name + " it has been added to your pack";
        texts[0] = textDialogue;
        textManager.textDialogues = texts;
        EventCore.EV_DisplayText.Invoke(textManager);
    }

    public void toggleOff()
    {
        this.enabled = false;
    }
}
