using UnityEngine;

interface IInteractable
{
    public bool toggleState {get; set;}
    public EventCore EventCore {get; set;}
    public void setupEventCore()
    {
        EventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
    }

    public bool IsThePlayerInteraction(Vector3 objectPosition, Vector3 HeroTransform)
    {
        if (Vector2.Distance(HeroTransform, objectPosition) < 0.5f) 
        {
            return true;
        }
        return false;
    }

    public void toggleOn();

    public void toggleOff();

}