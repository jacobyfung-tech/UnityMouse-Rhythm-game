using UnityEngine;
using System.Collections.Generic;
public class CalcDrumModel : MonoBehaviour
{

    public List<Vector3> points;
    public List<Vector3> basePoints;
    public List<Vector3> midPoints;
    public List<Vector3> connectorPoints;

    public float topThickness;
    //public bool loopPts; //whether last point should connect to first point, property in lineRenderer
    public LineRenderer topFace;
    public LineRenderer top2Face;
    public float fringeOffset;
    public float fringeSize;
    public LineRenderer botFace;
    public LineRenderer midFace;
    //to connect every top to bottom point we'd need like 100 linerenderers so we use a for loop to define that
    public LineRenderer baseFace;
    public LineRenderer connectorFace;
    public GameObject baseObj;



    public float segments;
    public float radius;

    public bool flag = false;

    public float jitter = 0.01f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       





    }

    // Update is called once per frame
    void setLineCounts(){
        topFace.positionCount = (int)segments;

        top2Face.positionCount = (int)segments;

        botFace.positionCount = (int)segments;

        midFace.positionCount = (int)segments;

        baseFace.positionCount = (int)segments;

        connectorFace.positionCount = (int)segments*3 / 2 - 1; //WHY

        topFace.widthMultiplier = VectorUtils.lineThickness;
        top2Face.widthMultiplier = VectorUtils.lineThickness;
        botFace.widthMultiplier = VectorUtils.lineThickness;
        midFace.widthMultiplier = VectorUtils.lineThickness;
        baseFace.widthMultiplier = VectorUtils.lineThickness;
        connectorFace.widthMultiplier = VectorUtils.lineThickness;
        
    }
    void Update(){ //order faces from top to bottom for readability
        setLineCounts();

        points.Clear();
        VectorUtils.DrawCircle(points, segments, radius, transform, 0f, topThickness);
        VectorUtils.AnimationJitter(points);

        topFace.SetPositions(points.ToArray());



        points.Clear();
        VectorUtils.DrawCircle(points, segments, radius*fringeSize, transform, 0f, topThickness);
        VectorUtils.AnimationJitter(points);

        top2Face.SetPositions(points.ToArray());
        


        points.Clear();
        VectorUtils.DrawCircle(points, segments, radius, transform, topThickness, 0); //jagged offset should pretend to connect to top and bottom face
        VectorUtils.AnimationJitter(points);

        midFace.SetPositions(points.ToArray());
        


        points.Clear();
        VectorUtils.DrawCircle(points, segments, radius, transform, 0f, -topThickness); //bottom of top face
        VectorUtils.AnimationJitter(points);

        botFace.SetPositions(points.ToArray());


        basePoints.Clear();
        VectorUtils.DrawCircle(basePoints, segments, radius*1.08f, baseObj.transform, 0f, 0f);
        VectorUtils.AnimationJitter(basePoints);

        baseFace.SetPositions(basePoints.ToArray());
        

        if (flag){
            return;
        }

        midPoints.Clear();
        VectorUtils.Connect2Faces(midPoints, points, basePoints, segments);
        VectorUtils.AnimationJitter(midPoints);

        connectorFace.SetPositions(midPoints.ToArray());
        


    }
}

