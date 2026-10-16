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



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        
        StartCoroutine(introAnim());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChangeAnimation(SpriteRenderer playerR, Sprite[] frames, float frameTime, float duration, object script){
        

        
        Debug.Log("animation should have started");
        AnimationObject anims = new AnimationObject(playerR, frames, frameTime, duration, (MonoBehaviour)script);
        anims.Play();
        yield return new WaitForSeconds(duration);
        Debug.Log("animation should have finished");

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

        while(lastCoroutine != null){
            yield return null;
        }

        hammerInput.playCharge();

        lastCoroutine = StartCoroutine(ChangeAnimation(playerR, keyPose2, frameTime, 2.5f, this));

        while(lastCoroutine != null){
            yield return null;
        }

        float originalFrameTime = frameTime;

        for (int i = 0; i < 16; i++){
            
            lastCoroutine = StartCoroutine(ChangeAnimation(playerR, hammerSwing, frameTime, hammerSwing.Length * frameTime, this));
            hammerInput.StartCoroutine(hammerInput.playHit(frameTime*5)); //hammer reaches true bottom in animation at frame 5
            hammerAnim.StartSwing(frameTime);
            while(lastCoroutine != null){
                yield return null;
            }
            hammerAnim.ResetHammer();

            lastCoroutine = StartCoroutine(ChangeAnimation(playerR, keyPose2, frameTime, 0.27f-(i/60f), this));

            while(lastCoroutine != null){
                yield return null;
            }
            frameTime /= 1.2f;
        }

        frameTime = originalFrameTime;

        lastCoroutine = StartCoroutine(ChangeAnimation(playerR, keyPose1, frameTime, 1.3f, this));

        while(lastCoroutine != null){
            yield return null;
        }

        Debug.Log("done with intro animation");

    }

}


