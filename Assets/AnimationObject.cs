using UnityEngine;
using System.Collections;
public class AnimationObject
{
    public Sprite[] sprites;
    public float frameTime;
    public float totalTime;
    public SpriteRenderer playerR;
    public MonoBehaviour runner;

    public Coroutine animationCoroutine;

    public AnimationObject(SpriteRenderer spriteRenderer, Sprite[] sprites, float frameTime, float totalTime, MonoBehaviour runner)
    {
        this.sprites = sprites;
        this.frameTime = frameTime;
        this.totalTime = totalTime;
        this.playerR = spriteRenderer;
        this.runner = runner;
    }
    public void Play()
    {
        animationCoroutine = AnimationUtils.playOnce(
        runner,
        playerR,
        sprites,
        frameTime,
        totalTime);
    
    } 

    public Coroutine GetCoroutine()
    {
        return animationCoroutine;
    }
}