using UnityEngine;

public class LatencyConstants : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static float calibrationOffset = -.12f; //later this will be modifiable by the user, but for now it is hardcoded
    public static float window = 0.05f; //both ways window 
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            calibrationOffset += 0.01f;
            Debug.Log("Calibration offset: " + calibrationOffset);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow)){
            calibrationOffset -= 0.01f;
            Debug.Log("Calibration offset: " + calibrationOffset);
        }
    }
}
