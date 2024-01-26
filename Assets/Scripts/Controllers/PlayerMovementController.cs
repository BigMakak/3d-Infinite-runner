using System;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Player Configurations")]
    public PlayerConfig playerConfig;

    [SerializeField] private LayerMask GroundMask;

    [Header("Player Visuals")]
    [SerializeField] private GameObject jumpParticles;

    private bool m_playedSFX = false;

    #region Internal Player Variables

    private bool m_isGrounded;

    //Jump related Variables
    private bool m_jump;

    private bool m_releaseJump;

    private Collider m_currCollider = null;

    private Rigidbody m_rb;

    // Behavior Dependencies
    private PlayerInputController m_playerInputController;

    #endregion

    void Awake()
    {
        m_playerInputController = GetComponent<PlayerInputController>();

        m_rb = GetComponent<Rigidbody>();

        if (!m_playerInputController)
        {
            Debug.LogError("No Player Input found, please add it to the Player Object");
            return;
        }
    }

    void Update()
    {
        checkGroundCollision();

        checkPlayerInput();
    }

    void FixedUpdate()
    {

        checkGroundCollision();

        if (m_jump)
        {
            AudioController.Instance.PlayAudio("Jump");
            //Reset internal value
            Jump();
            m_jump = false;
        }

        if (m_releaseJump)
        {
            Jump(true);
            m_releaseJump = false;
        }

        checkGravity();
    }

    #region Private Functions

    private void checkPlayerInput()
    {
        if (m_isGrounded)
        {
            if (m_playerInputController.m_jumpAction.WasPerformedThisFrame())
            {
                m_isGrounded = false;
                //m_velocity.y = playerConfig.JumpForce;
                m_jump = true;
            }
        }
        else
        {
            if (m_playerInputController.m_jumpAction.WasReleasedThisFrame())
            {
                m_releaseJump = true;
            }
        }
    }

    /// <summary>
    /// Adds a Force to the Player, for it to Jump or descend faster
    /// </summary>
    /// <param name="down">If the jump will have a downward force</param>
    private void Jump(bool down = false)
    {
        Vector3 _direction = Vector3.up;
        float _jumpForce = playerConfig.JumpForce;

        //We want to a apply a downward force to the player, to simulate a smaller jump
        if (down)
        {
            _direction = Vector3.down;
            _jumpForce = playerConfig.JumpForce / 3;
        }

        m_rb.AddForce(_direction * _jumpForce * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    /// <summary>
    /// Adds more Gravity if the player is Falling
    /// </summary>
    private void checkGravity()
    {
        if (m_rb.velocity.y > 0)
        {
            m_rb.mass = playerConfig.NormalMass;
        }
        else
        {
            m_rb.mass = playerConfig.NormalMass * playerConfig.MaxMultiplier;
        }
    }

    /// <summary>
    /// Checks if the player has hit the ground
    /// Adds points if it landed on a different block/platform
    /// </summary> 
    private void checkGroundCollision()
    {
        Vector3 _spherePosition = new Vector3(this.transform.position.x, this.transform.position.y - playerConfig.BottomThresholf, this.transform.position.z);
        Collider[] _rayCastResult = new Collider[5];
        int _hits = Physics.OverlapSphereNonAlloc(_spherePosition, playerConfig.SphereSize, _rayCastResult, GroundMask);

        //Debug.Log("Hits length: " + _hits);

        //The player lands on something
        if (_hits > 0)
        {
            Collider _currCollider = _rayCastResult[0].GetComponent<Collider>();

            playSFX();

            //If the collider is different, we count it as points for landing
            if (_currCollider != m_currCollider && m_currCollider != null)
            {
                GameController.Instance.AddPoints(playerConfig.PointsForLanding);
            }
        
            //Reset the necessary values for a second jump
            m_currCollider = _currCollider;
            m_isGrounded = true;
            m_rb.mass = playerConfig.NormalMass;
        }
        else
        {
            m_isGrounded = false;
            m_playedSFX = false;
        }
    }

    private void playSFX() 
    {
        if(!m_playedSFX) 
        {
            Instantiate(jumpParticles,this.transform).GetComponent<ParticleSystem>().Play();
            m_playedSFX = true;
        }
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 _spherePosition = new Vector3(this.transform.position.x, this.transform.position.y - playerConfig.BottomThresholf, this.transform.position.z);

        Gizmos.DrawWireSphere(_spherePosition, playerConfig.SphereSize);
    }

    #endregion
}
