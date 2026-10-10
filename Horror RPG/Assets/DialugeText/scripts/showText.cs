using UnityEngine;

public class showText : MonoBehaviour
{
    textTestScript _textScript;
    EventCore EventCore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _textScript = GetComponent<textTestScript>();
        EventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        EventCore.EV_DisplayText.AddListener(displayText);
    }

    private void displayText(textDisplayManager textManager)
    {
        _textScript.testText = textManager;
        _textScript.writeText();
    }
}
