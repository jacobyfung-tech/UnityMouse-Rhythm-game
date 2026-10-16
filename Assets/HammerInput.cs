using UnityEngine;
using System.Collections;
public class HammerInput : MonoBehaviour
{
    public Inputs inputHandler;
    public HammerAnimate hammerAnimator;
    public float mouseThreshold; //threshold for mouse movement to trigger hammer swing, i'd recommend 0.25

    public float lastDMouseX;
    public float lastDMouseY;
    public bool leftClickDown;

    public AudioSource audioSource;
    public AudioClip chargingSound;
    public AudioClip hitSound;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GameObject.FindWithTag("SoundObj").GetComponent<AudioSource>();
    }

    // Update is called once per frame

    public void playHit(){
        audioSource.volume = 1;
        audioSource.loop = false;
        audioSource.clip = hitSound;
        audioSource.Play();
    }

    public IEnumerator playHit(float delay){ //delay version
        yield return new WaitForSeconds(delay);
        audioSource.volume = 1;
        audioSource.loop = false;
        audioSource.clip = hitSound;
        audioSource.Play();
    }

    public void playCharge(){
        audioSource.loop = true;
        audioSource.clip = chargingSound; //after hitting the charging permanently cuts out so we reenable it
        audioSource.Play();
        StartCoroutine(fadeIn());
    }

    IEnumerator fadeIn(){
        audioSource.volume = 0;
        while (audioSource.volume < 0.3f){
            audioSource.volume += 0.04f;
            yield return new WaitForSeconds(0.01f);
        }
    }


    void Update()
    {


        leftClickDown = Input.GetMouseButton(0);
        float DMouseX = inputHandler.GetMouseX(); //currently unused but we should implement anyway
        float DMouseY = inputHandler.GetMouseY();

        if (
            DMouseX > mouseThreshold && 
            lastDMouseX <= mouseThreshold &&
            leftClickDown) //debounce
        {

            Debug.Log("Mouse moved up by " + DMouseX + ", swinging hammer.");

            playHit();


            hammerAnimator.StartSwing();
        }
        else if (
            DMouseX < -mouseThreshold/2 &&
            lastDMouseX >= -mouseThreshold/2 &&
            hammerAnimator.currentState != HammerAnimate.HammerState.Idle &&
            leftClickDown) //debounce
        {

            Debug.Log("Mouse moved down by " + DMouseX + ", resetting hammer.");
            hammerAnimator.ResetHammer();

            if (leftClickDown){
                playCharge();
            }
        }

        checkChargeSFX();



        lastDMouseX = DMouseX;
        lastDMouseY = DMouseY;
    }

    void checkChargeSFX(){
        if (!(hammerAnimator.currentState == HammerAnimate.HammerState.Idle || hammerAnimator.currentState == HammerAnimate.HammerState.Bottom)){
            return; //don't play charging sound if hammer is not idle
        }


        bool leftClickDownNow = Input.GetMouseButtonDown(0);
        if (leftClickDownNow){
            playCharge();
        }
        else if (Input.GetMouseButtonUp(0)){
            audioSource.loop = false;
            audioSource.Stop();
        }
    }
}
