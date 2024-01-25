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

    //Player current velocity vector
    private Vector3 m_velocity;

    // The accelaration of the player horizontally
    private float m_acceleration = 10f;

    // Behavior Dependencies
    private PlayerInputController m_playerInputController;

    private GameController m_gameController;

    #endregion

    void Awake()
    {
        m_playerInputController = GetComponent<PlayerInputController>();

        m_gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

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
        Vector3 _nextPos = transform.position;

        m_gameController.DistanceTravelled += m_velocity.x * Time.deltaTime;

        movePlayerVertically();

        movePlayerHorizontally(ref _nextPos);

        transform.position = _nextPos;
    }

    public Vector3 GetPlayerMovement() 
    {
        return this.m_velocity;
    }

    private void movePlayerHorizontally(ref Vector3 _initialPos)
    {
        if (m_isGrounded)
        {
            return;
        }

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

    private void movePlayerVertically()
    {
        if (!m_isGrounded)
        {
            return;
        }

        // Define a velocity ratio, so that player accelerates until a certain point
        float velocityRatio = m_velocity.x / playerConfig.MaxXVelocity;

        m_acceleration = playerConfig.MaxAcceleration * (1 - velocityRatio);

        //Increase the X value based on the current acceleration value
        m_velocity.x += m_acceleration * Time.fixedDeltaTime;

        //Limit the X value 
        if (m_velocity.x >= playerConfig.MaxXVelocity)
        {
            m_velocity.x = playerConfig.MaxXVelocity;
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
