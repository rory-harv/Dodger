using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float xAngle = 0f;
    [SerializeField] float yAngle = 0f;
    [SerializeField] float zAngle = 0f;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (!Dropper.paused && Scorer.started)
        {
            transform.Rotate(xAngle, yAngle, zAngle);
        }
    }
}
