using UnityEngine;

public class Explosion : MonoBehaviour
{

    public SpriteRenderer playerR;
    public Sprite[] frames;
    public float frameTime;
    public int cloned;
    public Vector3 oP;
    public int range = 4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //transform.localScale = new Vector3(Random.value+1,Random.value+1,transform.localScale.z);
        oP = transform.position;
        Explode();
    }

    void Explode(){
        AnimationObject anims = new AnimationObject(playerR, frames, frameTime, frames.Length*frameTime, (MonoBehaviour)this);
        anims.Play();
        //Invoke("Move",frames.Length*frameTime);
        
    }

    void Move(){
        transform.position = oP + new Vector3(Random.value*range - range/2,Random.value*range - range/2 ,Random.value*range - range/2);
        Explode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
