using UnityEngine;

public class DrawGizmos : MonoBehaviour
{
    public DrawStates drawStates;

    [SerializeField] private Color color;

    [SerializeField] private float size;

    void Awake()
    {
    }

 
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        switch (drawStates)
        {
            case DrawStates.DrawCube:
                Gizmos.DrawCube(this.transform.position, Vector3.one * size);
                break;
            case DrawStates.DrawSphere:
                Gizmos.DrawSphere(this.transform.position, size);
                break;
        }
        
    }
}

 public enum DrawStates
    {
        DrawCube,
        DrawSphere
    }