using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerator : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private List<GameObject> groundPrefabs = new List<GameObject>();

    [Header("Spawn Points")]
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private Transform farSpawnPoint;

    [Header("Configs")]
    [SerializeField] private ProceduralConfigs proceduralConfigs;
    [SerializeField] private GameConfigs gameConfigs;

    public void SpawnGround()
    {

         // Adjust block size based on timeLeft
        float _blockSizeMultiplier = Mathf.Lerp(proceduralConfigs.MinBlockSizeMultiplier, proceduralConfigs.MaxBlockSizeMultiplier, GameController.Instance.GetTimeLeft() / gameConfigs.GameTime);

        float _blockSpacing = Mathf.Lerp(farSpawnPoint.transform.position.x,spawnPoint.transform.position.x, GameController.Instance.GetTimeLeft() / gameConfigs.GameTime);

        //Debug.LogFormat("Block Size Multiplier: {0} || _BLOCKSPACING: {1}", _blockSizeMultiplier,_blockSpacing);

        GameObject _currObject = Instantiate(randomizePrefab(),spawnPoint.transform.position,Quaternion.identity);

        _currObject.transform.localScale *= randomizeSizeMultiplier(_blockSizeMultiplier);
        _currObject.transform.position = randomizePosition(_blockSpacing);
    }


    private GameObject randomizePrefab() 
    {
        //Get a random index between 0 and the max number of prefabs that exist on the list
        int _randomIndex = Random.Range(0,groundPrefabs.Count);
        
        return groundPrefabs[_randomIndex];
    }

    private float randomizeSizeMultiplier(float _currMultiplier)
    {
        return Random.Range(_currMultiplier - 0.2f,_currMultiplier);
    }

    private Vector3 randomizePosition(float _blockSpacing)
    {
        float _intialHeight = spawnPoint.transform.position.y;
        float _randomY = Random.Range(_intialHeight - proceduralConfigs.HeightDiscrepancy,_intialHeight + proceduralConfigs.HeightDiscrepancy);
        return new Vector3(_blockSpacing,_randomY,0f);
    }

}
