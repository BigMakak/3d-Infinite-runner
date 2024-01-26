using UnityEngine;

public class CoinBehavior : MonoBehaviour
{

    [SerializeField] private PlayerConfig playerConfig;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") 
        {
            AudioController.Instance.PlayAudio("Coin");
            
            GameController.Instance.AddPoints(playerConfig.PointsForCoins);

            Destroy(this.gameObject);
        }
    }
}
