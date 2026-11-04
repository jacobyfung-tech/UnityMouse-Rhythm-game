using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Collections;



public class NoteTracker : MonoBehaviour
{
    //JSON SHOULD REPLACE THESE BEFORE THIS STARTS
    public string levelTitle;
    public float BPM;
    public int[] levelChart;


    public double musicStartDSP = 0;
    public double elapsedTime = 0;
    public double nextBeatTiming = 0;
    public int beatsPassed = 1;

    public float sixteenthNote = 1;

    public static bool songPlaying = false;




    public float originalWobble;

    public PlayerHammer playerScript;


    public int levelNumber;

    public int QbeatsPassed = 0;

    public AudioSource calibrationSong;
    public AudioSource levelSong;

    public int lastQBeat;
    public int lastBeat;



    public float intensity = 4;
    
    public float rate = 0.26f;
    public AudioSource debugMetronome;



    //in the future, read BPM and chart from json

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalWobble = VectorUtils.globalWobble;






    }


    public IEnumerator startTutorial(){
        yield return new WaitForSeconds(0.4f);
        playerScript.StartIntro();
        //ReadJson();
        calibrationSong.Play();
        //BPM = 97; //comment out once json files are implemented
        yield return StartCoroutine(RealStart());
    }

    public IEnumerator startLevel(AudioClip song){
        yield return new WaitForSeconds(0.4f);
        yield return StartCoroutine(RealStart());
    }

    IEnumerator RealStart()
    {
        sixteenthNote = (0.25f / BPM) * 60f;
        nextBeatTiming = sixteenthNote;
        musicStartDSP = AudioSettings.dspTime;
        //yield return new WaitForSeconds(0.02f); //dsp time is calculated correctly but visuals are late
        levelSong.Play();
        songPlaying = true;
        yield return null;
    }


    void CheckIndex(int beatsPassed)
    {
        int currentIndex = levelChart[beatsPassed%levelChart.Length];
        debugMetronome.Play();   
    }
    void Update()
    {
        

        if (!songPlaying){
            return;
        }

        elapsedTime = AudioSettings.dspTime - musicStartDSP + LatencyConstants.calibrationOffset;

        beatsPassed = Mathf.FloorToInt((float)(elapsedTime / sixteenthNote));
        QbeatsPassed = Mathf.FloorToInt(beatsPassed/4f);


        if(QbeatsPassed != lastQBeat){
            //Debug.Log("hit");
            StartCoroutine(DecomposeGraphics());
            
        }

        while (lastBeat < beatsPassed)
        {
            lastBeat++;
            CheckIndex(lastBeat);   // pass the beat in, rather than reading beatsPassed
        }


        lastQBeat = Mathf.FloorToInt(beatsPassed/4f);
        //lastBeat = beatsPassed;
    }

    IEnumerator DecomposeGraphics(){
        
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
