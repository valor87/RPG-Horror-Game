using UnityEngine;

[CreateAssetMenu(fileName = "PlayerValues", menuName = "Scriptable Objects/PlayerValues")]
public class PlayerValues : ScriptableObject
{
    public int Gold;
    public int PerviousSceneNum;
    public Vector2 PerviousScenePos;

}
