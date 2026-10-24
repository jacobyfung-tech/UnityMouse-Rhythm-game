using UnityEngine;
using System.Collections;

//every animation object contains a sprite array, frame time, total time, and a reference to the sprite renderer and the script that is running it.
//next time you come back to this create a new animationobject for each keyframe and then play them in order with a coroutine. 

public class PlayerHammer : MonoBehaviour
{
    [Header("Sprite Animations")]
    public Sprite[] keyPose1;
    public Sprite[] transistion;
    public Sprite[] jumpscare;
    public Sprite[] keyPose2;
    public Sprite[] hammerSwing;
    public Sprite[] glance;

    [Header("Timing")]
    public float frameTime;
    public float timer;
    public float rotationSpeed;

    [Header("References")]
    public SpriteRenderer playerR;
    public HammerAnimate hammerAnim;
    public HammerInput hammerInput;
    public Explosion explosion;
    public GameObject entireObj;

    [Header("Cutscene State")]
    public Coroutine lastCoroutine;

    [Header("Hammer")]
    public HammerIdle hammerIdle;

    [Header("Positions")]
    public Vector3 originalPos;

    [Header("Fly Away")]
    public float flySpeed = 0.02f;

    [Header("Hammer Barrage Range")]
    public float xRange = 25f;
    public float zRange = 12f;

    [Header("Hammer Position")]
    public Vector3 realHammerPos;

    [Header("Audio")]
    public AudioClip suprised;
    public AudioClip goofySkedaddleSFX;

    [Header("Camera")]
    public float initialOffset;
    public Vector3 original;
    public GameObject camera;
    public float cameraLSpeed;
    public float zoomOrig;


    [Header("Wobble")]
    public float originalWobble;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        originalWobble = VectorUtils.globalWobbleMult;
        original = camera.transform.position;
        camera.transform.position = original+new Vector3(-initialOffset,0,0) + camera.transform.forward*zoomOrig;;
    }

    public void StartIntro()
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
    IEnumerator PanCamera(Vector3 original,float cameraLSpeed){
        for(int i = 0; i < 500; i++){ 
            camera.transform.position = Vector3.Lerp(camera.transform.position,original,cameraLSpeed);
            yield return null;
        }
        camera.transform.position = original;
    }

    IEnumerator Spin(){
        yield return new WaitForSeconds(0.65f);
        AudioSource.PlayClipAtPoint(goofySkedaddleSFX, Camera.main.transform.position, 0.4f);
        float rotationSpeedT = rotationSpeed;
        while (true){
            yield return null;
            entireObj.transform.Rotate(Vector3.up * rotationSpeedT);
            rotationSpeedT = Mathf.Min(rotationSpeedT * 1.08f, 420f);
            entireObj.transform.position += new Vector3(0,rotationSpeedT/14f,0);
        }
    }

    IEnumerator introAnim(){

        lastCoroutine = StartCoroutine(ChangeAnimation(playerR, keyPose1, frameTime, 1.8f, this));

        while(lastCoroutine != null){
            yield return null;
        }

        
        lastCoroutine = StartCoroutine(ChangeAnimation(playerR, transistion, frameTime, transistion.Length * frameTime, this));

        while(lastCoroutine != null){
            yield return null;
        }
        StartCoroutine(PanCamera(original,cameraLSpeed));

        AudioSource.PlayClipAtPoint(suprised, Camera.main.transform.position, 1.0f);
        lastCoroutine = StartCoroutine(ChangeAnimation(playerR, jumpscare, frameTime, 0.6f, this));

        Vector3 beforePos = playerR.transform.position;
        while(lastCoroutine != null){
            playerR.transform.position = Vector3.Lerp(playerR.transform.position, beforePos + new Vector3(-1,4,0),0.4f);
            yield return null;
        }

        for(int i = 0; i < 4; i++){ 
            playerR.transform.position = Vector3.Lerp(playerR.transform.position, beforePos,0.6f);
            yield return null;
        }
        playerR.transform.position = beforePos;


        lastCoroutine = StartCoroutine(ChangeAnimation(playerR, keyPose2, frameTime, 0.4f, this));

        while(lastCoroutine != null){
            yield return null;
        }

        Destroy(hammerIdle);    
        hammerInput.playCharge();
        hammerAnim.ResetHammer();

        lastCoroutine = StartCoroutine(ChangeAnimation(playerR, glance, frameTime, glance.Length*frameTime, this));

        while(lastCoroutine != null){
            yield return null;
        }




        float originalFrameTime = frameTime;

        realHammerPos = hammerAnim.transform.position;
        Vector3 hammerOffset = hammerAnim.originalPose.position - transform.position;

        frameTime *= 0.7f;

        for (int i = 0; i < 22; i++){
            lastCoroutine = StartCoroutine(ChangeAnimation(playerR, hammerSwing, frameTime, hammerSwing.Length * frameTime, this));
            hammerInput.StartCoroutine(hammerInput.playHit(frameTime*5)); //hammer reaches true bottom in animation at frame 5

            hammerAnim.StartSwing(frameTime);
            while(lastCoroutine != null){
                yield return null;
            }
            hammerAnim.ResetHammer();

            lastCoroutine = StartCoroutine(ChangeAnimation(playerR, keyPose2, frameTime, 0.27f-(i/60f), this));

            Vector3 newP;
            if (i < 21){
                newP = originalPos + new Vector3(Random.Range(0, xRange),0,Random.Range(-zRange,zRange));
            }
            else{
                newP = originalPos; //reset properly;
            }

            if (i == 20){
                explosion.Explode();
                StartCoroutine(Spin());
            }

            



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




        frameTime = originalFrameTime/2;

        lastCoroutine = StartCoroutine(ChangeAnimation(playerR, jumpscare, frameTime, 1.9f, this));

        Debug.Log("commence comedic flying");


        StartCoroutine(FlyAway());

        while(lastCoroutine != null){
            yield return null;
        }

        frameTime = originalFrameTime;

        //VectorUtils.globalWobbleMult = originalWobble;



    }

    IEnumerator FlyAway(){
        for(int i = 0; i < 45; i++){ 
            playerR.transform.position += new Vector3(Random.value/4 - .125f, Random.value/4 -.125f, 0);
            yield return null;
        }
        float originalFlySpeed = flySpeed;
        for(int i = 0; i < 155; i++){ 
            playerR.transform.position += new Vector3(-flySpeed+Random.value/4 - .125f, flySpeed/2+Random.value/4 - .125f, 0);
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
        GameState.canInput = true;
    }

}


