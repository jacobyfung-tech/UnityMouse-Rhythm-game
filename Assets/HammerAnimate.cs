using UnityEngine;
using System.Collections;
public class HammerAnimate : MonoBehaviour
{
    private int[] rotationFrames = {2,3,4,10,63,-20,-3,-2,-1,0,0,0,0,0,0}; //manually defined rotation frames because rhythm games need to be deterministic
    public GameObject pivot;
    public float yDropSpeed = 0.1f;
    public Vector3 originalRotation;
    
    
    public Pose originalPose = new Pose(
        Vector3.zero,
        Quaternion.identity
    );
  
    public enum HammerState
    { 
        Idle,
        Swinging,
        Bottom,
        Resetting
    }
    private HammerState currentState;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HammerState currentState = HammerState.Idle;
        originalPose = new Pose(
            transform.position,
            transform.rotation
        );
        //StartCoroutine(Animate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartSwing()
    {
        if (currentState == HammerState.Idle)
        {
            StartCoroutine(Animate());
        }
        else{
            transform.SetPositionAndRotation(//discard previous frames of rotation and position
                originalPose.position,
                originalPose.rotation
            );
            StartCoroutine(Animate());

        }
    }

    IEnumerator Animate()
    {
        currentState = HammerState.Swinging;
        for (int i = 0; i < rotationFrames.Length; i++)
        {
            transform.RotateAround(pivot.transform.position, pivot.transform.forward, -rotationFrames[i]);
            transform.parent.position += yDropSpeed * Vector3.down * Time.deltaTime * rotationFrames[i]; //proportional to 
            yield return null;
        }
        currentState = HammerState.Bottom;
        yield return new WaitForSeconds(0.3f);
        currentState = HammerState.Resetting;
        transform.SetPositionAndRotation(
            originalPose.position,
            originalPose.rotation
        );

    }
}
