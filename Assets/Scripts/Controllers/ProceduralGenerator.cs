using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerator : MonoBehaviour
{

    [SerializeField] private GameObject groundPrefab;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private Transform farSpawnPoint;

    [SerializeField] private ProceduralConfigs proceduralConfigs;

    private Queue<GameObject> m_grounds;

    public float maxTime = 60f; // Set the maximum time for your game


    void Awake()
    {
        m_grounds = new Queue<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnGround(Vector3 _lastPosition)
    {

         // Adjust block size based on timeLeft
        float _blockSizeMultiplier = Mathf.Lerp(proceduralConfigs.minBlockSizeMultiplier, proceduralConfigs.maxBlockSizeMultiplier, GameController.Instance.GetTimeLeft() / maxTime);

        float _blockSpacing = Mathf.Lerp(farSpawnPoint.transform.position.x,spawnPoint.transform.position.x, GameController.Instance.GetTimeLeft() / maxTime);

        Debug.LogFormat("Block Size Multiplier: {0} || _BLOCKSPACING: {1}", _blockSizeMultiplier,_blockSpacing);

        GameObject _currObject = Instantiate(groundPrefab,spawnPoint.transform.position,Quaternion.identity);

        _currObject.transform.localScale *= randomizeSizeMultiplier(_blockSizeMultiplier);
        _currObject.transform.position = randomizePosition(_blockSpacing);
    }

    private float randomizeSizeMultiplier(float _currMultiplier)
    {
        
        return Random.Range(_currMultiplier - 0.2f,_currMultiplier);
    }

    private Vector3 randomizePosition(float _blockSpacing)
    {
        float _randomY = Random.Range(proceduralConfigs.minBlockHeight,proceduralConfigs.maxBlockHeight);
        return new Vector3(_blockSpacing,_randomY,0f);
    }

}
