using UnityEngine;

public class stairs : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<OverWorldPlayerMovement>().onStars = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<OverWorldPlayerMovement>().onStars = false;
    }
}
