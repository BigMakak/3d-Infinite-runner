using UnityEngine;

[CreateAssetMenu(fileName = "Create a new Procedural Config", menuName = "Configs/Procedural Config")]
public class ProceduralConfigs : ScriptableObject
{

    public float minBlockSizeMultiplier = 0.6f; // Minimum block size
    public float maxBlockSizeMultiplier = 1.2f; // Maximum block size

    public float maxBlockHeight = -5;

    public float minBlockHeight = -10;

}
