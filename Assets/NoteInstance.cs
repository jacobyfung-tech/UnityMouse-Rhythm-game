using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Collections;



public class NoteInstance : MonoBehaviour
{
    //top 3 values MUST be set by rhythm tracker
    [Header ("Set by tracker")]
    public float initY;
    public float timeUntilHit; 
    public GameObject target; //we need to use d = rt to get here in sync with song
    public int frames;

    //in the minigame, notes are scripted to be spawned in 1 quarter note BEFORE you input with an audio cue 
    public float window;
    public float calibrationOffset;

    [Header ("Should be readonly")]
    
    public bool dropped = false;
    public float createdT;
    public Vector3 rate;
    public bool missed;
    public Vector3 originalPos;
        public enum state{
        Waiting,
        HitWindowOpen,
        HitWindowClosed

    }
    public state currentState;

    public AudioSource debugMetronome;


    //in the future, create a script that constantly checks for every gameobject... 
    //...with tags associated with notes in cases where player inputs.

    void Awake(){
        originalPos = transform.position;
    }

    void Start(){
        debugMetronome = gameObject.GetComponent<AudioSource>();
        currentState = state.Waiting;
        transform.rotation = target.transform.rotation;

        currentState = state.Waiting;
        createdT = Time.time;
        rate = (target.transform.position - transform.position)/timeUntilHit;
        StartCoroutine(Fall());
    }

    IEnumerator Fall()
    {
        dropped = false;
        transform.position += new Vector3(0,initY,0);
        float bounce = 3.9f;
        Vector3 origin = transform.position;
        Vector3 floor = transform.position - new Vector3(0,initY+bounce,0);
        for(int i = 0; i < frames; i++)
        {
            Vector3 next = Vector3.Lerp(origin,floor,(i + 1) / (float)frames);
            transform.position = new Vector3(transform.position.x, next.y, transform.position.z);
            yield return null;
        }

        floor = transform.position + new Vector3(0,bounce,0);
        for(int i = 0; i < 8; i++)
        {
            Vector3 next = Vector3.Lerp(transform.position,floor,(i + 1) / (float)frames);
            transform.position = new Vector3(transform.position.x, next.y, transform.position.z);
            yield return null;
        }

        dropped = true;
    }

    void Update(){
        float elapsed = (float)(Time.timeAsDouble - createdT);
        float lowerWindow = createdT + timeUntilHit - window;
        float upperWindow = createdT + timeUntilHit + window;
        
        if(Time.timeAsDouble > lowerWindow
        && Time.timeAsDouble < upperWindow
        && currentState != state.HitWindowOpen){
            //debugMetronome.Play();
            Debug.Log("inputstart on " + gameObject.name);
            currentState = state.HitWindowOpen;
        }
        else if(Time.timeAsDouble >= upperWindow && currentState != state.HitWindowClosed){
            //debugMetronome.Play();
            Debug.Log("inputstart end " + gameObject.name);
            currentState = state.HitWindowClosed;
            Invoke("delete",3);
        }
     
  


        Vector3 next = originalPos + rate * elapsed;
        transform.position = new Vector3(next.x,transform.position.y,next.z);
        
        

    }
    void delete()
    {
        Destroy(gameObject);
    }
}