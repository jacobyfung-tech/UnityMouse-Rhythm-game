using UnityEngine;

public class HammerIdle : MonoBehaviour
{
    [Header("Sine Settings")]
    public float amplitude = 0.25f;   // how far it moves from the center
    public float frequency = 1f;      // cycles per second
    public float phaseOffset = 0f;    // shift the wave so multiple objects don't sync
    public Vector3 axis = Vector3.up; // direction of movement (local space)
    public float rotSpeed;
    public float yOffset;

    private Vector3 startLocalPos;
    private Vector3 startGlobPos;

    void Start()
    {
        startLocalPos = transform.localPosition;
        startGlobPos = transform.position;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {

        
        float offset = Mathf.Sin(Time.time * frequency * Mathf.PI * 2f + phaseOffset) * amplitude;
        
        //transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime, Space.World); 

        transform.localPosition = startLocalPos + (axis.normalized * offset) + new Vector3(0,yOffset,0);
        
       
    }
    public void ResetPose()
    {
        transform.localPosition = startLocalPos;
    }
}