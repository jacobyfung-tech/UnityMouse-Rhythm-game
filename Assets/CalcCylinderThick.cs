using UnityEngine;
using System.Collections.Generic;
[ExecuteAlways]
public class CalcCylinderThick : MonoBehaviour
{
    public List<Vector3> TPoints;
    public List<Vector3> BPoints;
    public List<Vector3> midPoints;

    public LineRenderer topFace;
    public LineRenderer botFace;
    public LineRenderer connectorFace;

    public float jitter;

    public float segments;
    public float radius;
    

    public float height;

    public Transform lower;

    private int frameCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        topFace.widthMultiplier = VectorUtils.lineThickness;
        botFace.widthMultiplier = VectorUtils.lineThickness;
        connectorFace.widthMultiplier = VectorUtils.lineThickness;
    }

    // Update is called once per frame
    void setLineCounts(){
        topFace.positionCount = (int)segments;
        botFace.positionCount = (int)segments;
        connectorFace.positionCount = (int)segments * 3/2 -1;
    
        topFace.widthMultiplier = VectorUtils.lineThickness;
        botFace.widthMultiplier = VectorUtils.lineThickness;
        connectorFace.widthMultiplier = VectorUtils.lineThickness;
    }

    void Update()
    {

        frameCount++;
        if(frameCount % VectorUtils.renderEvery != 0){
            return;
        }

        setLineCounts();

        TPoints.Clear();
        VectorUtils.DrawCircle(TPoints, segments, radius, transform);
        VectorUtils.AnimationJitter(TPoints);

        topFace.SetPositions(TPoints.ToArray());



        BPoints.Clear();
        VectorUtils.DrawCircle(BPoints, segments, radius, lower);
        VectorUtils.AnimationJitter(BPoints);

        botFace.SetPositions(BPoints.ToArray());

        midPoints.Clear();
        VectorUtils.Connect2Faces(midPoints, TPoints, BPoints, segments);
        VectorUtils.AnimationJitter(midPoints);

        connectorFace.SetPositions(midPoints.ToArray());
        
    }

    
}





