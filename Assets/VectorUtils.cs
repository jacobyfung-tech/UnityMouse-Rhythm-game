using UnityEngine;
using System.Collections.Generic;
public static class VectorUtils{ //static so future scripts can access this function without needing to create an instance of the class or redundant code


    public static readonly float globalWobbleOriginal = 0.02f;
    public static float globalWobble = globalWobbleOriginal;
    public static float globalWobbleMult = 1f;
    public static float lineThickness = 1.2f;

    public static int corners = 3;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetStatics() { globalWobble = globalWobbleOriginal; }
    public static void Init()
    {
        globalWobble = globalWobbleOriginal;
    }

    public static void DrawCircle(List<Vector3> points, float segments, float radius, Transform transform){
        for(int segment = 0; segment < segments; segment++){

            Vector3 circleOffset = new Vector3(Mathf.Sin(segment/segments * 2 * Mathf.PI) * radius, 0,Mathf.Cos(segment/segments * 2 * Mathf.PI) * radius);
            points.Add(transform.TransformPoint(circleOffset));
            //Debug.Log(circleOffset);
        }
    }

    public static void DrawCircleQuarter(List<Vector3> points, int segments, float radius, Transform transform, Vector3 center, float startTurns)
    {
        for (int i = 0; i <= segments; i++)
        {
            float angle = (startTurns + (i / (float)segments) * 0.25f) * 2f * Mathf.PI;
            Vector3 local = center + new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * radius;
            points.Add(transform.TransformPoint(local));
        }
    }
    public static void DrawCircle(List<Vector3> points, float segments, float radius, Transform transform, float jaggedOffset){ //if jagged
        for(int segment = 0; segment < segments; segment++){

            Vector3 circleOffset = new Vector3(Mathf.Sin(segment/segments * 2 * Mathf.PI) * radius, jaggedOffset,Mathf.Cos(segment/segments * 2 * Mathf.PI) * radius);
            points.Add(transform.TransformPoint(circleOffset));
            //Debug.Log(circleOffset);

            jaggedOffset = -jaggedOffset; //flip the offset for the next point
        }
    }

    public static void DrawCircle(List<Vector3> points, float segments, float radius, Transform transform, float jaggedOffset, float yOffset){ //if jagged and y offset
        for(int segment = 0; segment < segments; segment++){

            Vector3 circleOffset = new Vector3(Mathf.Sin(segment/segments * 2 * Mathf.PI) * radius, yOffset + jaggedOffset,Mathf.Cos(segment/segments * 2 * Mathf.PI) * radius);
            points.Add(transform.TransformPoint(circleOffset));
            //Debug.Log(circleOffset);

            jaggedOffset = -jaggedOffset;
        }
    }

    public static void AnimationJitter(List<Vector3> points){ //randomize point positions slightly for lineboil effect
        float r = VectorUtils.globalWobble * VectorUtils.globalWobbleMult;
        if (r <= 0f) return;   // no wobble, no work
        float wobble = VectorUtils.globalWobble;

        for(int i = 0; i < points.Count; i++){
            points[i] += new Vector3(
            Random.Range(-r, r), 
            Random.Range(-r, r), 
            Random.Range(-r, r));
        }

        
    }


    public static void Connect2Faces(List<Vector3> midFace, List<Vector3> face1, List<Vector3> face2, float segmentsC)
    {
   
        bool UP = false;
        bool lastUP = false;
        for (int segment = 0; segment < segmentsC; segment++)
        {
            
            if (segment % 2 == 0){
                UP = !UP;
            }

            if (UP && !lastUP){
                midFace.Add(face1[segment]);
                midFace.Add(face2[segment]);
            }
            else if (!UP && lastUP){
                midFace.Add(face2[segment]);
                midFace.Add(face1[segment]);
            }
            else if (UP && lastUP){
                midFace.Add(face2[segment]);
            }
            else if (!UP && !lastUP ){
                midFace.Add(face1[segment]);
            }


            lastUP = UP;


        } //alternate 1 renderer to give illusion because i am NOT making 500 seperate linerenderers for each connector
    }
    
}
