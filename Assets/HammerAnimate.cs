using UnityEngine;
using System.Collections;
public class HammerAnimate : MonoBehaviour
{
    private int[] rotationFrames = {2,6,13,26,31,-8,-3,-3,-2,-2,-1,0,0,0,0}; //manually defined rotation frames because rhythm games need to be deterministic
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
    [SerializeField] HammerState currentState;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = HammerState.Idle;
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
            Debug.Log("you have to reset the hammer first before swinging again.");
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


        if (currentState == HammerState.Resetting){
            yield break; //avoid being at "bottom" while resetting because this prevents swinging
        }
        if (currentState == HammerState.Idle){
            yield break; //avoid being at "bottom" while resetting because this prevents swinging
        }
        currentState = HammerState.Bottom;
        


    }

    public void ResetHammer()
    {
        currentState = HammerState.Resetting;
        StartCoroutine(resetSmoothly());
    
    }

    IEnumerator resetSmoothly(){
        currentState = HammerState.Resetting;
        for (int x = 0; x < 3; x++)
        {
            transform.position = Vector3.Lerp(transform.position, originalPose.position, 0.7f);
            transform.rotation = Quaternion.Lerp(transform.rotation, originalPose.rotation, 0.7f);
            yield return new WaitForSeconds(0.01f); //will round up at 60fps, but also prevents higher framerates from making the animation too fast.
        }

        transform.SetPositionAndRotation(
            originalPose.position,
            originalPose.rotation
        );
        currentState = HammerState.Idle;
    }
}
