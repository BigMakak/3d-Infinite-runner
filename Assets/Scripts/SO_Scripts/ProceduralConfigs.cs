using UnityEngine;

[CreateAssetMenu(fileName = "Create a new Procedural Config", menuName = "Configs/Procedural Config")]
public class ProceduralConfigs : ScriptableObject
{

    public float MinBlockSizeMultiplier = 0.6f; // Minimum block size
    public float MaxBlockSizeMultiplier = 1.2f; // Maximum block size

    public float HeightDiscrepancy = 5; // The discprancy between block heigths

}
