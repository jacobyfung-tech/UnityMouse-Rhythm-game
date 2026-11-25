using UnityEngine;
using System.Collections.Generic;


public class Conveyor : MonoBehaviour
{
    public List<Vector3> leftPoints;
    public List<Vector3> rightPoints;
    public List<Vector3> midPoints;

    public float width;
    public float distancePerSpan;
    
    public LineRenderer connectorFace;
    public int segments;
    public int midSegments;
    public float givenSpeed;
    public Vector3 originalPos;
    //public Vector3 rate;
    public float start = Time.time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setLineCounts();    
        originalPos = transform.position;
    }
    void setLineCounts(){
        midSegments = segments + (int)Mathf.Ceil(segments / 2);
        connectorFace.positionCount = (int)segments*3/2;
        connectorFace.widthMultiplier = VectorUtils.lineThickness;

        leftPoints.Clear();
        rightPoints.Clear();
        for(int i = 0; i < segments; i++)
        {
            leftPoints.Add(transform.TransformPoint(new Vector3(width/2, 0,+distancePerSpan*i)));
            rightPoints.Add(transform.TransformPoint(new Vector3(-width/2, 0,distancePerSpan*i)));
        }
    }
    void Update(){ //order faces from top to bottom for readability

        if (!Stagger.Tick(transform))
        {
            return;
        }
        setLineCounts();

        midPoints.Clear();
        VectorUtils.Connect2Faces(midPoints, leftPoints, rightPoints, segments);
        VectorUtils.AnimationJitter(midPoints);

        connectorFace.SetPositions(midPoints.ToArray());

        float period = 8f * distancePerSpan;                     
        double frac = ((Time.time - start) * givenSpeed) % 1.0;
        transform.position = originalPos - transform.forward * (float)frac * period;
    }
}
