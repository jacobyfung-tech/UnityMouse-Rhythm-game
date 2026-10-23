using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Collections;



public class NoteInstance : MonoBehaviour
{
    public float duration;
    public float calibrationOffset;
    public double createdT;
    public GameObject target; //we need to use d = rt to get here in sync with song
    public Vector3 rate;
    public bool missed;
    public Vector3 originalPos;
    public enum state{
        Waiting,
        HitWindowOpen,
        HitWindowClosed

    }
    public state currentState;

    void Awake(){
        originalPos = transform.position;
    }

    void Start(){
        currentState = state.Waiting;
        createdT = Time.timeAsDouble;
        rate = (target.transform.position - transform.position)/duration;
    }

    void Update(){
        float elapsed = (float)(Time.timeAsDouble - createdT);
        if(Time.timeAsDouble > createdT + duration - calibrationOffset && Time.timeAsDouble < createdT + duration + calibrationOffset){
            currentState = state.HitWindowOpen;
        }
        else if(Time.timeAsDouble >= createdT + duration + calibrationOffset){
            currentState = state.HitWindowClosed;
        }
        else{
            currentState = state.Waiting;
            
        }
        transform.position = originalPos + rate * elapsed;

    }
}