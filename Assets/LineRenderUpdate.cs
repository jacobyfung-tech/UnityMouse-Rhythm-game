using UnityEngine;
using System.Collections.Generic;
public class LineRenderUpdate : MonoBehaviour
{
    public Transform[] objects;

    public Vector3[] points;
    //public bool loopPts; //whether last point should connect to first point, property in lineRenderer
    public LineRenderer LR;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        LR = gameObject.GetComponent<LineRenderer>();

        points = new Vector3[objects.Length];



    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < objects.Length; i++){
            points[i] = objects[i].position;
        }
        SetPositions(points);
    }

    void SetPositions(Vector3[] points)
    {
        LR.positionCount = points.Length;
        LR.SetPositions(points);
    }

}
