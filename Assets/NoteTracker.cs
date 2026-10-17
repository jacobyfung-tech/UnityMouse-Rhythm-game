using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Collections;



public class NoteTracker : MonoBehaviour
{

    public double musicStartDSP = 0;
    public double elapsedTime = 0;
    public double nextBeatTiming = 0;
    public int beatsPassed = 1;

    public float sixteenthNote = 1;

    public bool songPlaying = false;

    public float BPM;

    public int levelNumber;

    public int QbeatsPassed = 0;

    public AudioSource calibrationSong;

    public int lastQBeat;



    public float intensity = 4;
    
    public float rate = 0.26f;

    //in the future, read BPM and chart from json

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {



        //ReadJson();
        calibrationSong.Play();


        BPM = 97; //comment out once json files are implemented


        sixteenthNote = (0.25f / BPM) * 60f;
        nextBeatTiming = sixteenthNote;
        Invoke("StartMusic", 0.5f);
    }

    void StartMusic()
    {

        musicStartDSP = AudioSettings.dspTime;
        songPlaying = true;

        Invoke("stringme", 0.1f);
    }



    // Update is called once per frame
    void Update()
    {
        

        if (!songPlaying){
            return;
        }

        elapsedTime = AudioSettings.dspTime - musicStartDSP;
        beatsPassed = Mathf.FloorToInt((float)(elapsedTime / sixteenthNote));
        QbeatsPassed = Mathf.FloorToInt(beatsPassed/4f);


        if(QbeatsPassed != lastQBeat){
            //Debug.Log("hit");
            StartCoroutine(DecomposeGraphics());
        }



        lastQBeat = Mathf.FloorToInt(beatsPassed/4f);
    }

    IEnumerator DecomposeGraphics(){
        float originalWobble = VectorUtils.globalWobble;
        float tWobble = VectorUtils.globalWobble * intensity;

        for (int i = 0; i < 10; i++){
            VectorUtils.globalWobble = tWobble;

            tWobble = Mathf.Lerp(tWobble, originalWobble, rate);
            
            //Debug.Log("set current wobble to " + tWobble);

            yield return null;
        }

        VectorUtils.globalWobble = originalWobble;
    }
}
