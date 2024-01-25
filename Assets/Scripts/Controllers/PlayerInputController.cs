using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{

    public bool Jump { get; private set; }

    public PlayerConfig playerConfig;

    // Player Input Internal Variables
    private PlayerInput m_playerInput;

    private InputAction m_jumpAction;

    private Timer m_holdJumpTimer;

    private bool holdingJump = false;


    void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();

        m_jumpAction = m_playerInput.actions["Jump"];

        m_holdJumpTimer = new Timer(playerConfig.HoldJumpTime);

        m_holdJumpTimer.OnTimerEnd += onHoldJumpTimeOut;
    }
   
    // Update is called once per frame
    void Update()
    {
        if(Jump)
            m_holdJumpTimer.Tick(Time.deltaTime); 

        handlePlayerInput();
    }


    public void ResetValues() 
    {
        Jump = false;
        holdingJump = false;
        m_holdJumpTimer.Reset();
    }

    private void handlePlayerInput()
    {
        if (m_jumpAction.WasPressedThisFrame() && !holdingJump)
        {
            Jump = true;
            holdingJump = true;
        }

        if (m_jumpAction.WasReleasedThisFrame())
        {
            Jump = false;
        }
    }

    private void onHoldJumpTimeOut() 
    {
        Jump = false;
    }

    void OnDisable()
    {
        m_holdJumpTimer.OnTimerEnd -= onHoldJumpTimeOut;
    }
}
