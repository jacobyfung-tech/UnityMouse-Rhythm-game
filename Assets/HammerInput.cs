using UnityEngine;

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
    void Update()
    {
        leftClickDown = Input.GetMouseButton(0);
        float DMouseX = inputHandler.GetMouseX(); //currently unused but we should implement anyway
        float DMouseY = inputHandler.GetMouseY();

        if (
            DMouseY > mouseThreshold && 
            lastDMouseY <= mouseThreshold &&
            leftClickDown) //debounce
        {

            Debug.Log("Mouse moved up by " + DMouseY + ", swinging hammer.");

            audioSource.loop = false;
            audioSource.clip = hitSound;
            audioSource.Play();

            hammerAnimator.StartSwing();
        }
        else if (
            DMouseY < -mouseThreshold/10 &&
            lastDMouseY >= -mouseThreshold/10 &&
            leftClickDown) //debounce
        {

            Debug.Log("Mouse moved down by " + DMouseY + ", resetting hammer.");
            hammerAnimator.ResetHammer();

            if (leftClickDown){
                audioSource.loop = false;
                audioSource.clip = chargingSound; //after hitting the charging permanently cuts out so we reenable it
                audioSource.Play();
            }
        }

        checkChargeSFX();



        lastDMouseX = DMouseX;
        lastDMouseY = DMouseY;
    }

    void checkChargeSFX(){
        if (!(hammerAnimator.currentState == HammerAnimate.HammerState.Idle)){
            return; //don't play charging sound if hammer is not idle
        }


        bool leftClickDownNow = Input.GetMouseButtonDown(0);
        if (leftClickDownNow){
            audioSource.loop = true;
            audioSource.clip = chargingSound;
            audioSource.Play();
        }
        else if (Input.GetMouseButtonUp(0)){
            audioSource.loop = false;
            audioSource.Stop();
        }
    }
}
