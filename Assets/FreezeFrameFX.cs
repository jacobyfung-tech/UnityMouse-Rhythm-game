using UnityEngine;
using System.Collections.Generic;

public class FreezeFrameFX : MonoBehaviour
{
    [SerializeField] private int delayFrames = 10;
    [SerializeField] private Transform target;

    [SerializeField] private readonly Queue<Vector3> history = new Queue<Vector3>();
    [SerializeField] private readonly Queue<Vector3> historyRot = new Queue<Vector3>();
    //transform.rotation = Quaternion.Euler(0f, 90f, 0f);


    [SerializeField] private bool followRotation = true;

    public int frameGone = 0;

    void Start()
    {
        // prefill at target
        for (int i = 0; i < delayFrames; i++){
            history.Enqueue(target.position);
            historyRot.Enqueue(target.rotation.eulerAngles);
        }
    }

    void LateUpdate()
    {
        history.Enqueue(target.position);
        transform.position = history.Dequeue();
        if (followRotation == false){
            return;
        }

        historyRot.Enqueue(target.rotation.eulerAngles);
        transform.eulerAngles = historyRot.Dequeue();
    }
}