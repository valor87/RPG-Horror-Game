using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform PlayerTransform;
    Vector3 CameraScreenSpace;
    float CameraRightSideBuffer;
    float CameraLeftSideBuffer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CameraScreenSpace.x = Screen.width;
        CameraScreenSpace.y = Screen.height;

        CameraLeftSideBuffer = (CameraScreenSpace.x / 2) / 2;
        CameraRightSideBuffer = CameraScreenSpace.x - (CameraScreenSpace.x / 4);
    }

    // Update is called once per frame
    void Update()
    {
        MoveCameraToFindPlayer(PlayerTransform);
    }

    void MoveCameraToFindPlayer(Transform PlayerTransform)
    {
        Vector3 PlayerInScreenSpace;

        PlayerInScreenSpace = Camera.main.WorldToScreenPoint(PlayerTransform.position);


        if (PlayerInScreenSpace.x < CameraLeftSideBuffer)
        {
            print("Left most side of the screen");
        }
        if (PlayerInScreenSpace.x > CameraRightSideBuffer)
        {
            print("Right most side of the screen");
        }
    }
}
