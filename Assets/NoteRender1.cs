using UnityEngine;
using System.Collections.Generic;
[ExecuteAlways]
public class RectanglePrismRound : MonoBehaviour
{

    public List<Vector3> pointsTop;
    public List<Vector3> pointsBottom;
    public List<Vector3> pointsConnector;


    public float topThickness;
    //public bool loopPts; //whether last point should connect to first point, property in lineRenderer
    public LineRenderer botFace;
    public LineRenderer topFace;
    public LineRenderer connectorFace;

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

    void AddCorners(List<Vector3> points,Vector3 offset){
        VectorUtils.DrawCircleQuarter(points, pointsPerCorner, radius, transform,new Vector3(edgeSize+offset.x,0+offset.y,edgeSize+offset.z),0f);
        VectorUtils.DrawCircleQuarter(points, pointsPerCorner, radius, transform,new Vector3(edgeSize+offset.x,0+offset.y,-edgeSize+offset.z),0.25f);
        VectorUtils.DrawCircleQuarter(points, pointsPerCorner, radius, transform,new Vector3(-edgeSize+offset.x,0+offset.y,-edgeSize+offset.z),.5f);
        VectorUtils.DrawCircleQuarter(points, pointsPerCorner, radius, transform,new Vector3(-edgeSize+offset.x,0+offset.y,edgeSize+offset.z),.75f);
    }

    void Update(){ 
        botFace.positionCount = pointsPerCorner * 4;
        pointsBottom.Clear();
        AddCorners(pointsBottom,new Vector3(0,0,0));
        VectorUtils.AnimationJitter(pointsBottom);
        botFace.positionCount = pointsBottom.Count;
        botFace.SetPositions(pointsBottom.ToArray());

        topFace.positionCount = pointsPerCorner * 4;
        pointsTop.Clear();
        AddCorners(pointsTop,new Vector3(0,2,0));
        VectorUtils.AnimationJitter(pointsTop);
        topFace.positionCount = pointsTop.Count;
        topFace.SetPositions(pointsTop.ToArray());

        pointsConnector.Clear();
        VectorUtils.Connect2Faces(pointsConnector, pointsBottom, pointsTop, pointsBottom.Count);
        VectorUtils.AnimationJitter(pointsConnector);

        connectorFace.positionCount = pointsConnector.Count;
        connectorFace.SetPositions(pointsConnector.ToArray());

    }
}

