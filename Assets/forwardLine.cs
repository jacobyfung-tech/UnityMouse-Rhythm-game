using UnityEngine;

public class forwardLine : MonoBehaviour
{
    public float offset;

    private LineRenderer line;

    public Color lineColor; 
    public Color lineColorEnd; 

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.startColor = lineColor;
        line.endColor = lineColorEnd;
    }

    void Update()
    {
        Vector3 p1 = transform.position;
        line.SetPosition(0, p1);
        line.SetPosition(1, p1 + offset*transform.forward);
        line.startColor = lineColor;
        line.endColor = lineColorEnd;
    }
}