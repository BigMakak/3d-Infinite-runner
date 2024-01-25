using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName ="Create New Player Config", menuName ="Configs/Player Config")]
/// <summary>
/// Player Configuration Variables 
/// </summary>
public class PlayerConfig : ScriptableObject
{
    public float Gravity = -10;

    public float JumpForce = 20;

    public float HoldJumpTime = 0.35f;

    public float JumpThreshold = 3f;
}
