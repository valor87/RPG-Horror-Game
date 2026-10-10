using UnityEngine;

interface IInteractable
{
    public bool toggleState {get; set;}
    public float interactionDistance { get; set;}
    public EventCore EventCore {get; set;}
    public void setupEventCore()
    {
        EventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
    }

    public bool IsThePlayerInteraction(Vector3 objectPosition, Vector3 HeroTransform)
    {
        if (Vector2.Distance(HeroTransform, objectPosition) < interactionDistance) 
        {
            return true;
        }
        return false;
    }

    public void toggleOn();

    public void toggleOff();

}