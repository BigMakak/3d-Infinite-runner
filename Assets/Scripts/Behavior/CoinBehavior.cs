using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") 
        {
            GameController.Instance.AddPoints(500);

            Destroy(this.gameObject);
        }
    }
}
