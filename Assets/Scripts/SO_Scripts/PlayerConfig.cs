using UnityEngine;

[CreateAssetMenu(fileName ="Create New Player Config", menuName ="Configs/Player Config")]
/// <summary>
/// Player Configuration Variables 
/// </summary>
public class PlayerConfig : ScriptableObject
{

    [Header("Player Vertical Movement Variables")]
    public float Gravity = -10; // Gravity force applied to the player vertically

    public float JumpForce = 20; // The force that is applied to the player when jumping

    public float HoldJumpTime = 0.35f; // The time in seconds, that the player can hold the jump button

    public float JumpThreshold = 3f; // The distance in blocks, that the player can jump again before reaching the ground

    [Header("Player Horizontal Movement Variables")]

    public float MaxAcceleration = 10f; // Reference vlaue for the max accelaration for the player

    // The maximum X velocity that the player can reach
    public float MaxXVelocity = 100f; 
}
