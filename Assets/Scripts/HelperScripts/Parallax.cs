using UnityEngine;
using UnityEngine.UI;

public class Parallax : MonoBehaviour
{
    [SerializeField] private int Depth;

    private const float VALUE_DAMP = 0.01f;

    //Internal Variables

    private RawImage m_rawImage;

    // Start is called before the first frame update
    void Start()
    {
        //Setup the texture values and intial position for reference
        m_rawImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        scroll();
    }

    private void scroll() 
    {
        //Scroll the image with a new rect, determined by the player X Movement Value
        float _newXPos = GameController.Instance.GetCurrentSpeed() * VALUE_DAMP / Depth;

        m_rawImage.uvRect = new Rect(m_rawImage.uvRect.position + new Vector2(_newXPos,0f) * Time.deltaTime,m_rawImage.uvRect.size);
    }
}
