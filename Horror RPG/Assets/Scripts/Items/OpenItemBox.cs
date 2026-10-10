using UnityEngine;
using UnityEngine.Rendering;

public class OpenItemBox : MonoBehaviour, IInteractable
{
    [Header("Item to give")]
    [SerializeField]
    private ItemsObjects itemToGive;
    [SerializeField]
    private Sprite boxSpriteChange;

    public bool toggleState { get; set; }
    public EventCore EventCore { get; set; }

    IInteractable _Iinteractable;

    void Start()
    {
        _Iinteractable = GetComponent<IInteractable>();
        _Iinteractable.setupEventCore();
        EventCore.EV_ObjectInteract.AddListener(checkPlayerInteraction);
    }

    void checkPlayerInteraction(Vector3 heroPosition)
    {
        _Iinteractable.IsThePlayerInteraction(transform.position, heroPosition);
    }

    public void toggleOn()
    {
        EventCore.EV_ItemPickUp.Invoke(itemToGive);
        GetComponent<SpriteRenderer>().sprite = boxSpriteChange;
    }

    public void toggleOff()
    {

    }
}
