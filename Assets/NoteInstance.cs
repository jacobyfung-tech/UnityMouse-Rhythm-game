using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Collections;



public class NoteInstance : MonoBehaviour
{
    public double duration;
    public double calibrationOffset;
    public double createdT;
    public GameObject target; //we need to use d = rt to get here in sync with song
    public Vector3 rate;
    public bool missed;
    public Enum state{
        Waiting,
        HitWindowOpen,
        HitWindowClosed,

    }
    public state currentState;

    void Start(){
        currentState = state.Waiting;
        createdT = Time.timeAsDouble;
        rate = (target.transform.position - transform.position)/duration;
    }

    void Update(){
        if(Time.timeAsDouble > createdT + duration - calibrationOffset && Time.timeAsDouble < createdT + duration + calibrationOffset){
            currentState = state.HitWindowOpen;
        }
        else if(Time.timeAsDouble >= createdT + duration + calibrationOffset){
            currentState = state.HitWindowClosed;
        }
        else{
            currentState = state.Waiting;
            transform.position += rate * Time.deltaTime;
        }

    }
}