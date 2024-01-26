using System;
using UnityEngine;

public class GameController : MonoBehaviour
{

    //External Global Values
    public static GameController Instance { get; private set; }

    public float DistanceTravelled { get; private set; }

    public float Points { get; private set; }

    //Global Events to the Main Game
    public event Action OnGameEnd;

    public GameConfigs gameConfigs;

    //Internal variables
    private Timer m_gameTimer;

    private float m_currSpeed;

    private bool m_isStoped = false;

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

        m_gameTimer.OnTimerEnd += EndGame;
    }

    void Update()
    {
        if(m_isStoped)
            return;

        m_gameTimer.Tick(Time.deltaTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(m_isStoped) 
            return;

        increaseGameSpeed();
    }

    public void EndGame() 
    {
        AudioController.Instance.PlayAudio("Lose");

        //Reset values and stop the game from running
        m_isStoped = true;
        m_currSpeed = 0;
        OnGameEnd?.Invoke();
    }


    private void increaseGameSpeed()
    {
        // Define a velocity ratio, so that player accelerates until a certain point
        float velocityRatio = m_currSpeed / gameConfigs.MaxXVelocity;

        //The game Accelerates has the time goes on
        float _acceleration = Mathf.Abs(m_gameTimer.RemaingSeconds - gameConfigs.GameTime);

        _acceleration *= 1 - velocityRatio;
        //Increase the X value based on the current acceleration value
        m_currSpeed += _acceleration * Time.fixedDeltaTime;

        incrementGameValues(m_currSpeed);

        //Limit the current X speed of the game 
        if (m_currSpeed >= gameConfigs.MaxXVelocity)
        {
            m_currSpeed = gameConfigs.MaxXVelocity;
        }
    }

    private void incrementGameValues(float _currSpeed)
    {

        //Calculate Distance travelled based on passed time
        DistanceTravelled += _currSpeed * Mathf.Lerp(1, 0, GetTimeLeft() / gameConfigs.GameTime);

        //The counter will increment if the ration is meet
        int incrementCounter = Mathf.FloorToInt(_currSpeed / gameConfigs.PointsRatio);

        Debug.Log(incrementCounter);

        // Check if there has been an increment
        if (incrementCounter > 0)
        {
            //Add points to the game
            AddPoints(incrementCounter);
        }
    }

    
    public void AddPoints(int _points)
    {
        this.Points += _points;
    }


    public float GetCurrentSpeed()
    {
        return this.m_currSpeed;
    }


    public float GetTimeLeft()
    {
        return this.m_gameTimer.RemaingSeconds;
    }


    void OnDisable()
    {
        m_gameTimer.OnTimerEnd -= EndGame;
    }
}
