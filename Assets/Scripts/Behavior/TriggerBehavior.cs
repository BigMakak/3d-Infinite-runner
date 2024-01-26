using UnityEngine;
using UnityEngine.Events;

public class TriggerBehavior : MonoBehaviour
{

    public UnityEvent<Vector3> OnTriggerActivated;

    void OnTriggerEnter(Collider other)
    {
        OnTriggerActivated?.Invoke(this.transform.position);
    }
}
