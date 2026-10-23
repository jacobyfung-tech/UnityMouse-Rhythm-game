using UnityEngine;
using System.Collections.Generic;
public class bgLine : MonoBehaviour
{
    private LineRenderer line;

    public Color lineColor;
    public Color lineColorEnd;

    public float pointDist;
    public float drawDist = 500f;
    public int pointC = 50;

    private Vector3[] points; // private so the Inspector can't override its length
    private List<Vector3> vectorList;

    void Awake()
    {
        
        pointDist = drawDist / pointC;
        points = new Vector3[pointC];
        line = GetComponent<LineRenderer>();
        line.positionCount = pointC;
        line.startColor = lineColor;
        line.endColor = lineColorEnd;
        vectorList = new List<Vector3>(pointC);   // this was missing
        line.widthMultiplier = VectorUtils.lineThickness;
    }

    void Update()
    {
        line.widthMultiplier = VectorUtils.lineThickness;
        Vector3 p1 = transform.position;
        for (int i = 0; i < points.Length; i++)
            points[i] = p1 + pointDist * i * transform.right;

        vectorList.Clear();
        vectorList.AddRange(points);
        VectorUtils.AnimationJitter(vectorList);

        vectorList.CopyTo(points);
        line.SetPositions(points);
    }
}