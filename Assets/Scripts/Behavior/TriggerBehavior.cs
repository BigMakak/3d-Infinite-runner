using UnityEngine;
using UnityEngine.Events;

public class TriggerBehavior : MonoBehaviour
{
    [SerializeField] private string Tag = "";

    public UnityEvent OnTriggerActivated;

    void OnTriggerEnter(Collider other)
    {
        if(string.IsNullOrEmpty(Tag)) 
        {
            OnTriggerActivated?.Invoke();
            return;
        }

        if(other.gameObject.tag == Tag) 
        {
            OnTriggerActivated?.Invoke();
        }
    }
}
