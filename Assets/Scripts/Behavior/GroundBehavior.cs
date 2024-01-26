using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBehavior : MonoBehaviour
{

    public float GroundSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
       Vector3 _currPoss = transform.position;
       _currPoss.x -= GroundSpeed * GameController.Instance.GetCurrentSpeed() * Time.deltaTime;

        transform.position = _currPoss;
    }

    
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
