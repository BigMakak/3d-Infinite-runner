using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController Instance {get; private set;}

    public float DistanceTravelled { get; set; }

    public GameConfigs gameConfigs;

    private Timer m_gameTimer;

    private float m_currSpeed;

    void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        } 

        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_gameTimer = new Timer(gameConfigs.GameTime);
    }

    void Update()
    {
        m_gameTimer.Tick(Time.deltaTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        increaseGameSpeed();
    }


    public float GetCurrentSpeed() 
    {
        return this.m_currSpeed;
    }


    public float GetTimeLeft() 
    {
        return this.m_gameTimer.RemaingSeconds;
    }


    private void increaseGameSpeed()
    {
        // Define a velocity ratio, so that player accelerates until a certain point
        float velocityRatio = m_currSpeed / gameConfigs.MaxXVelocity;

        float _acceleration = Mathf.Abs(m_gameTimer.RemaingSeconds - gameConfigs.GameTime); 

        _acceleration *= 1 - velocityRatio;

        //Increase the X value based on the current acceleration value
        m_currSpeed += _acceleration * Time.fixedDeltaTime;

        //Limit the X value 
        if (m_currSpeed >= gameConfigs.MaxXVelocity)
        {
            m_currSpeed = gameConfigs.MaxXVelocity;
        }
    }
}
