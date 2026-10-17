using UnityEngine;

public class Explosion : MonoBehaviour
{

    public SpriteRenderer playerR;
    public Sprite[] frames;
    public float frameTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //transform.localScale = new Vector3(Random.value+1,Random.value+1,transform.localScale.z);
        AnimationObject anims = new AnimationObject(playerR, frames, frameTime, frames.Length*frameTime, (MonoBehaviour)this);
        anims.Play();
        Invoke("destroy",frames.Length*frameTime);
    }

    void destroy(){
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
