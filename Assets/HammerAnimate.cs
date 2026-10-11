using UnityEngine;
using System.Collections;
public class HammerAnimate : MonoBehaviour
{
    private int[] rotationFrames = {2,3,6,10,63,-20,-3,-2,-1,0,0,0,0,0,0}; //manually defined rotation frames because rhythm games need to be deterministic
    public GameObject pivot;
    public float yDropSpeed = 0.1f;
    public Vector3 originalRotation;
    
    public Pose originalPose = new Pose(
        Vector3.zero,
        Quaternion.identity
    );
  



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPose = new Pose(
            transform.position,
            transform.rotation
        );
        StartCoroutine(Animate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Animate()
    {
        while(true){
            yield return new WaitForSeconds(0.25f);
            for (int i = 0; i < rotationFrames.Length; i++)
            {
                transform.RotateAround(pivot.transform.position, pivot.transform.forward, -rotationFrames[i]);
                transform.parent.position += yDropSpeed * Vector3.down * Time.deltaTime * rotationFrames[i]; //proportional to 
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
            transform.SetPositionAndRotation(
                originalPose.position,
                originalPose.rotation
            );
        }

    }
}
