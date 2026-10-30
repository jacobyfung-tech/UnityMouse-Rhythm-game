using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Collections;



public class NoteInstance : MonoBehaviour
{
    //top 3 values MUST be set by rhythm tracker
    [Header ("Set by tracker")]
    public float timeUntilHit; 
    //in the minigame, notes are scripted to be spawned in 1 quarter note BEFORE you input with an audio cue 
    public float window;
    public float calibrationOffset;

    [Header ("Should be readonly")]
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


    //in the future, create a script that constantly checks for every gameobject... 
    //...with tags associated with notes in cases where player inputs.

    void Awake(){
        originalPos = transform.position;
    }

    void Start(){
        currentState = state.Waiting;
        createdT = Time.timeAsDouble;
        rate = (target.transform.position - transform.position)/timeUntilHit;
    }

    void Update(){
        float elapsed = (float)(Time.timeAsDouble - createdT);
        float lowerWindow = createdT + timeUntilHit + calibrationOffset - window;
        float upperWindow = createdT + timeUntilHit + calibrationOffset + window;
        
        if(Time.timeAsDouble > lowerWindow
        && Time.timeAsDouble < upperWindow){
            currentState = state.HitWindowOpen;
        }
        else if(Time.timeAsDouble >= upperWindow){
            currentState = state.HitWindowClosed;
        }
        else{
            currentState = state.Waiting;
            
        }
        transform.position = originalPos + rate * elapsed;

    }
}