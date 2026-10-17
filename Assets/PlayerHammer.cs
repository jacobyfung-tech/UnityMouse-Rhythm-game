using UnityEngine;
using System.Collections;

//every animation object contains a sprite array, frame time, total time, and a reference to the sprite renderer and the script that is running it.
//next time you come back to this create a new animationobject for each keyframe and then play them in order with a coroutine. 

public class PlayerHammer : MonoBehaviour
{

    public Sprite[] keyPose1;
    public Sprite[] transistion;
    public Sprite[] jumpscare;
    public Sprite[] keyPose2;
    public Sprite[] hammerSwing;

    public float frameTime;
    public float timer;

    public SpriteRenderer playerR;
    public HammerAnimate hammerAnim;
    public HammerInput hammerInput;

    public Coroutine lastCoroutine;

    public bool canHammerInput = false;

    public HammerIdle hammerIdle;

    public Vector3 originalPos;

    public float flySpeed = 0.02f;

    public float xRange = 31f;
    public float zRange = 12f;

    public Vector3 realHammerPos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        realHammerPos = hammerAnim.transform.position;
        originalPos = playerR.transform.position;
        StartCoroutine(introAnim());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChangeAnimation(SpriteRenderer playerR, Sprite[] frames, float frameTime, float duration, object script){
        

        
        //Debug.Log("animation should have started");
        AnimationObject anims = new AnimationObject(playerR, frames, frameTime, duration, (MonoBehaviour)script);
        anims.Play();
        yield return new WaitForSeconds(duration);
        //Debug.Log("animation should have finished");

        lastCoroutine = null;
        
    }


    IEnumerator introAnim(){
        lastCoroutine = StartCoroutine(ChangeAnimation(playerR, keyPose1, frameTime, 2.1f, this));

        while(lastCoroutine != null){
            yield return null;
        }

        lastCoroutine = StartCoroutine(ChangeAnimation(playerR, transistion, frameTime, transistion.Length * frameTime, this));

        while(lastCoroutine != null){
            yield return null;
        }

        lastCoroutine = StartCoroutine(ChangeAnimation(playerR, jumpscare, frameTime, 0.6f, this));
        transform.position += new Vector3(-2,1,0);
        while(lastCoroutine != null){
            yield return null;
        }
        playerR.transform.position = originalPos;


        lastCoroutine = StartCoroutine(ChangeAnimation(playerR, keyPose2, frameTime, 2.5f, this));

        while(lastCoroutine != null){
            yield return null;
        }

        Destroy(hammerIdle);    
        hammerInput.playCharge();
        hammerAnim.ResetHammer();

        yield return new WaitForSeconds(frameTime);

        float originalFrameTime = frameTime;

        realHammerPos = hammerAnim.transform.position;
        Vector3 hammerOffset = hammerAnim.originalPose.position - transform.position;

        for (int i = 0; i < 22; i++){
            lastCoroutine = StartCoroutine(ChangeAnimation(playerR, hammerSwing, frameTime, hammerSwing.Length * frameTime, this));
            hammerInput.StartCoroutine(hammerInput.playHit(frameTime*5)); //hammer reaches true bottom in animation at frame 5
            hammerAnim.StartSwing(frameTime);
            while(lastCoroutine != null){
                yield return null;
            }
            hammerAnim.ResetHammer();

            lastCoroutine = StartCoroutine(ChangeAnimation(playerR, keyPose2, frameTime, 0.27f-(i/60f), this));

            Vector3 newP = originalPos + new Vector3(Random.Range(0, xRange),0,Random.Range(-zRange,zRange));
            



            while (lastCoroutine != null) {
                yield return null;
                transform.position = Vector3.Lerp(transform.position, newP, 0.65f);
                hammerAnim.originalPose = new Pose(transform.position + hammerOffset, hammerAnim.originalPose.rotation);
                hammerAnim.transform.position = transform.position + hammerOffset; // keep the visible hammer in sync too
            }
            transform.position = newP;
            hammerAnim.originalPose = new Pose(newP + hammerOffset, hammerAnim.originalPose.rotation);
            frameTime /= 1.25f;
        }

        frameTime = 0.01f;

        lastCoroutine = StartCoroutine(ChangeAnimation(playerR, jumpscare, frameTime, 1.9f, this));

        Debug.Log("commence comedic flying");

        StartCoroutine(FlyAway());

        while(lastCoroutine != null){
            yield return null;
        }

        frameTime = originalFrameTime;





    }

    IEnumerator FlyAway(){
        float originalFlySpeed = flySpeed;
        for(int i = 0; i < 155; i++){ 
            playerR.transform.position += new Vector3(-flySpeed+Random.value/4, flySpeed/2+Random.value/4, 0);
            flySpeed *= 1.1f;
            yield return null;
        }
        flySpeed = originalFlySpeed;

        yield return new WaitForSeconds(0.25f);

        for(int i = 0; i < 15; i++){ 
            playerR.transform.position = Vector3.Lerp(playerR.transform.position, originalPos, 0.6f);
            yield return null;
        }
        playerR.transform.position = originalPos;
        hammerAnim.transform.position = realHammerPos;
    }

}


