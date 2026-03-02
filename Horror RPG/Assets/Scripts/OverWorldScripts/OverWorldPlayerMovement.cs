using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverWorldPlayerMovement : MonoBehaviour
{
    public KeyCode TurnEncountersOff;
    public bool encounterState;
    [Header("Player Values")]
    public Vector2 playerPos;
    public int PlayerGold;
    public int SceneNum;
    public PlayerValues PlayerValues;
    [Space(5)]
    [Header("Player Movement")]
    [Range(0,20)]
    [SerializeField] float MovementSpeed;
    Vector3 Movement;
    public float EncounterRandomNum;
    //Animation
    GameObject GameManager;
    SpriteRenderer PlayerSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
       // SceneManager.LoadScene(PV.PerviousSceneNum);
        transform.position = PlayerValues.PerviousScenePos;
        PlayerGold = PlayerValues.Gold;
    }
    void Start()
    {
        EncounterRandomNum = Random.RandomRange(5, 10);
        GameManager = GameObject.Find("DontDestroyGameManager").gameObject;
        PlayerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(TurnEncountersOff))
        {
            encounterState = !encounterState;
        }
        PlayerSprite.sprite = GameManager.GetComponent<CurrentHerosInParty>().HerosInScene[0].Spriteimage;
        // get player input and movement
        Movement.x = Input.GetAxisRaw("Horizontal");
        PlayerMovement(Movement, MovementSpeed);
        PlayerAnimation(Movement);
    }

    private void PlayerMovement(Vector3 MovementAmount, float Speed)
    {
        transform.position += MovementAmount * Speed * Time.deltaTime;
        if (MovementAmount.x != 0 && encounterState)
        {
            EncounterRandomNum -= Time.deltaTime;
            if (EncounterRandomNum <= 1)
            {
                SceneManager.LoadScene("CombatScene");
            }
        }
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
    private void OnDisable()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;

        PlayerValues.PerviousSceneNum = scene;
        playerPos = transform.position;
        PlayerValues.PerviousScenePos = playerPos;
        PlayerValues.Gold = PlayerGold;
    }
   
}
