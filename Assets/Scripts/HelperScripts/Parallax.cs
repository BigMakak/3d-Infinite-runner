using UnityEngine;
using UnityEngine.UI;

public class Parallax : MonoBehaviour
{
    [SerializeField] private int Depth;

    [SerializeField] private const float VALUEDAMP = 0.01f;

    //Internal Variables

    private PlayerMovementController m_playerMove;

    private RawImage m_rawImage;

    // Start is called before the first frame update
    void Start()
    {
        m_playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementController>();
        //Setup the texture values and intial position for reference
        m_rawImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
    }

    void Scroll() 
    {
        //Scroll the image with a new rect, determined by the player X Movement Value
        float _newXPos = m_playerMove.GetPlayerMovement().x * VALUEDAMP / Depth;

        m_rawImage.uvRect = new Rect(m_rawImage.uvRect.position + new Vector2(_newXPos,0f) * Time.deltaTime,m_rawImage.uvRect.size);
    }
}
