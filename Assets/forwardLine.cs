using UnityEngine;

public class bgLine : MonoBehaviour
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
        //line.enabled = !line.enabled;
        Vector3 p1 = transform.position;
        line.SetPosition(0, p1);
        line.SetPosition(1, p1 + offset*transform.right);
        line.startColor = lineColor;
        line.endColor = lineColorEnd;
    }
}