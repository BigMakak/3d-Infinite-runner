using UnityEngine;

public class GroundBehavior : MonoBehaviour
{

    private const float GROUNDSPEED = 0.5f;

    void FixedUpdate()
    {
        //The Ground will speed up has the Game it self builds up more speed
       Vector3 _currPoss = transform.position;
       _currPoss.x -= GROUNDSPEED * GameController.Instance.GetCurrentSpeed() * Time.deltaTime;

        transform.position = _currPoss;
    }

    
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
