using UnityEngine;
using UnityEngine.Rendering;

public class OverWorldPlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [Range(0,20)]
    [SerializeField] float MovementSpeed;
    Vector3 Movement;

    //Animation
    SpriteRenderer PlayerSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // get player input and movement
        Movement.x = Input.GetAxisRaw("Horizontal");
        PlayerMovement(Movement, MovementSpeed);
        PlayerAnimation(Movement);
    }

    private void PlayerMovement(Vector3 MovementAmount, float Speed)
    {
        transform.position += MovementAmount * Speed * Time.deltaTime;
    }
    private void PlayerAnimation(Vector3 MovementAmount)
    {
        if (MovementAmount.x < 0)
        {
            PlayerSprite.flipX = true;
            return;
        }
        PlayerSprite.flipX = false;
    }
}
