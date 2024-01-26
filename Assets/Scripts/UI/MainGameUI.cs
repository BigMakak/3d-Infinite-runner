using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainGameUI : MonoBehaviour
{
    [Header("Text Fields")]
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text DistanceText;

    [SerializeField] private TMP_Text TimeRemaing;

    [Header("Menus")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private TMP_Text finalScore;

    // Start is called before the first frame update
    void Start()
    {
        if(!ScoreText || !DistanceText) 
        {
            Debug.LogWarning("No text variables assigned, pleased add Them");
        }

        GameController.Instance.OnGameEnd += showGameOverMenu;     
    }

    // Update is called once per frame
    void Update()
    {
        DistanceText.text = (int)GameController.Instance.DistanceTravelled + "M";

        TimeRemaing.text = (int)GameController.Instance.GetTimeLeft() + " seconds";

        ScoreText.text = "Score : " + GameController.Instance.Points;
    }

    private void showGameOverMenu() 
    {
        mainMenu.SetActive(true);

        finalScore.text = finalScore.text + "\n" + GameController.Instance.Points;
    }

    void OnDisable()
    {
        GameController.Instance.OnGameEnd -= showGameOverMenu;
    }
}
