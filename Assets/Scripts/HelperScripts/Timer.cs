using System;

public class Timer
{
    
    public float RemaingSeconds {get; private set;}

    private float m_internalSeconds;

    public Timer(float RemaingSeconds) 
    {
        this.RemaingSeconds = RemaingSeconds;
        m_internalSeconds = RemaingSeconds;
    }
    
    public event Action OnTimerEnd;

    public void Tick(float deltaTime) 
    {
        CheckForTimerEnd();

        if(RemaingSeconds <= 0f) { return; }

        RemaingSeconds -= deltaTime;
    }

    /// <summary>
    /// Resets the timer to the initial defined time
    /// </summary>
    public void Reset() 
    {
        RemaingSeconds = m_internalSeconds;
    }

    public void Reset(float seconds) 
    {
        RemaingSeconds = seconds;
        m_internalSeconds = seconds;
    } 

    private void CheckForTimerEnd()
    {
        if(RemaingSeconds > 0f) {return;}

        RemaingSeconds = 0;

        OnTimerEnd?.Invoke();
    }   
}
