using UnityEngine;

[CreateAssetMenu(fileName ="Create New Player Config", menuName ="Configs/Player Config")]
/// <summary>
/// Player Configuration Variables 
/// </summary>
public class PlayerConfig : ScriptableObject
{

    [Header("Player Vertical Movement Variables")]
    public float NormalMass = 1; // Gravity force applied to the player vertically

    public int MaxMultiplier = 3;

    public float JumpForce = 450; // The force that is applied to the player when jumping

    public float BottomThresholf = 1f; // The size of the sphere to check collisions

    public float SphereSize = 0.3f;

    [Header("Point Variables")]
    public int PointsForLanding = 50;

    public int PointsForCoins = 1000;
}
