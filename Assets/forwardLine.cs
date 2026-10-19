using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
        ApplyWobble();
    }

    void ApplyWobble(){
        List<Vector3> positions =  new List<Vector3> {line.GetPosition(0),line.GetPosition(1)};


        line.GetPositions(positions.ToArray());

        VectorUtils.AnimationJitter(positions);

        line.SetPositions(positions.ToArray());
    }
}