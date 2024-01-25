using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public PlayerConfig playerConfig;

    #region Internal Player Variables

    private bool m_isGrounded;
    private float m_groundHeight = 0;

    private Vector3 m_velocity;

    // Input specific variables
    private PlayerInputController m_playerInputController;

    #endregion

    void Awake()
    {
        m_playerInputController = GetComponent<PlayerInputController>();

        if (!m_playerInputController)
        {
            Debug.LogError("No Player Input found, please add it to the Player Object");
            return;
        }    
    }

    void Update()
    {
        checkPlayerInput();
    }

    void FixedUpdate()
    {
        if (m_isGrounded)
        {
            return;
        }

        Vector3 _nextPos = transform.position;

        movePlayer(ref _nextPos);

        transform.position = _nextPos;
    }

    private void movePlayer(ref Vector3 _initialPos)
    {
        //Change the horizontal speed of the player
        _initialPos.y += m_velocity.y * Time.fixedDeltaTime;
        //Alter the speed by applying Gravity to the horizontal velocity of the player
        if (!m_playerInputController.Jump)
            m_velocity.y += playerConfig.Gravity * Time.fixedDeltaTime;

        //! Temporary code for checking ground collision
        if (_initialPos.y <= m_groundHeight)
        {
            _initialPos.y = m_groundHeight;
            m_isGrounded = true;
            m_playerInputController.ResetValues();
        }
    }

    private void checkPlayerInput() 
    {
         //Add a small threshold to the jump, so that the player can jump just has the model is about to hit the ground
        float groundDistance = Math.Abs(transform.position.y - m_groundHeight);

        if (m_isGrounded || groundDistance <= playerConfig.JumpThreshold)
        {
            if (m_playerInputController.Jump)
            {
                m_isGrounded = false;
                m_velocity.y = playerConfig.JumpForce;
            }
        }
    }
}