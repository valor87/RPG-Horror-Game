using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform PlayerTransform;
    public float CamSpeed;
    Vector3 CameraScreenSpace;
    float CameraRightSideBuffer;
    float CameraLeftSideBuffer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }Adding 

    // Update is called once per frame
    void Update()
    {
        print(Input.mousePosition);
        MoveCameraToFindPlayer(PlayerTransform);
    }

    void MoveCameraToFindPlayer(Transform PlayerTransform)
    {
        CameraScreenSpace.x = Screen.width;
        CameraScreenSpace.y = Screen.height;

        CameraLeftSideBuffer = (CameraScreenSpace.x / 2) / 2;
        CameraRightSideBuffer = (CameraScreenSpace.x / 2) + (CameraScreenSpace.x / 4);
        Vector3 PlayerInScreenSpace;

        PlayerInScreenSpace = Camera.main.WorldToScreenPoint(PlayerTransform.position);


        if (PlayerInScreenSpace.x < CameraLeftSideBuffer)
        {
            transform.position -= new Vector3(CamSpeed,0,0) * Time.deltaTime;
            print("Left most side of the screen");
        }
        if (PlayerInScreenSpace.x > CameraRightSideBuffer)
        {
            transform.position += new Vector3(CamSpeed, 0, 0) * Time.deltaTime;
            print("Right most side of the screen");
        }
    }
}
