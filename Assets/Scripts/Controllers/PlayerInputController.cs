using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{

    // Player Input Internal Variables
    private PlayerInput m_playerInput;

    public InputAction m_jumpAction;


    void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();

        m_jumpAction = m_playerInput.actions["Jump"];

    }
}
