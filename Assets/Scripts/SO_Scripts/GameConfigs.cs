using UnityEngine;

[CreateAssetMenu(fileName ="Create a new Game Configs", menuName ="Configs/Game Config")]
public class GameConfigs : ScriptableObject
{
    [Header("Game Configurations")]

    // The maximum X velocity that the player can reach
    public float MaxXVelocity = 80f; 

    public float GameTime = 60f;

    public int PointsRatio = 100;
}
