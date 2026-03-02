using UnityEngine;

public class ScreenSizeAdjustment : MonoBehaviour
{
    [SerializeField]
    private float dersiredWidth;
    [SerializeField]
    private float dersiredHeight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Adjust();
    }

    private void Adjust()
    {
        float targetAspect = dersiredWidth / dersiredHeight;
        float windowAspect = (float)Screen.width / (float)Screen.height;

        float scaleHeight = windowAspect / targetAspect;

        Camera camera = GetComponent<Camera>();

        if (scaleHeight < 1)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = camera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}
