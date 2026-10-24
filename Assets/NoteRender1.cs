using UnityEngine;
using System.Collections.Generic;
[ExecuteAlways]
public class NoteRender1 : MonoBehaviour
{

    public List<Vector3> points;
    public List<Vector3> pointsBottom;
    public List<Vector3> pointsConnector;


    public float topThickness;
    //public bool loopPts; //whether last point should connect to first point, property in lineRenderer
    public LineRenderer topFace;

    public int pointsPerCorner;
    public float radius;

    public bool flag = false;

    public float jitter = 0.01f;

    public float edgeSize = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       





    }

    // Update is called once per frame
    void setLineCounts(){

    }

    void AddCorners(){
        VectorUtils.DrawCircleQuarter(points, 8, radius, transform,new Vector3(edgeSize,0,edgeSize),0f);
        VectorUtils.DrawCircleQuarter(points, 8, radius, transform,new Vector3(edgeSize,0,-edgeSize),0.25f);
        VectorUtils.DrawCircleQuarter(points, 8, radius, transform,new Vector3(-edgeSize,0,-edgeSize),.5f);
        VectorUtils.DrawCircleQuarter(points, 8, radius, transform,new Vector3(-edgeSize,0,edgeSize),.75f);
    }

    void Update(){ 
        topFace.positionCount = pointsPerCorner * 4;
        points.Clear();
        AddCorners();

        VectorUtils.AnimationJitter(points);

        topFace.positionCount = points.Count;
        topFace.SetPositions(points.ToArray());

    }
}

