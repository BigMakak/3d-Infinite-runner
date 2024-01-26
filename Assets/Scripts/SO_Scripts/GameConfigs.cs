using UnityEngine;

[CreateAssetMenu(fileName ="Create a new Game Configs", menuName ="Configs/Game Config")]
public class GameConfigs : ScriptableObject
{
    [Header("Game Configurations")]

    public float MaxAcceleration = 10f; // Reference vlaue for the max accelaration for the player

    // The maximum X velocity that the player can reach
    public float MaxXVelocity = 80f; 

    public float GameTime = 60f;
}
