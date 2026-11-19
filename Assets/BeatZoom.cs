using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class CameraPulse : MonoBehaviour
{
    static float orig = 0;
    static float inward = 0;
    static float mult = 2;
    Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        orig = cam.fieldOfView;
        inward = orig * mult;

        Debug.Log(orig);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Zoom(){
        float orig = cam.fieldOfView;
        float inward = cam.fieldOfView-20;
        float curr = orig;

        for (int i = 0; i < 10; i++){

            curr = Mathf.Lerp(curr, inward, 0.1f);
            cam.fieldOfView = curr;
            //Debug.Log("set current wobble to " + tWobble);

            yield return null;
        }

        cam.fieldOfView = orig;

        yield return null;
    }
}
