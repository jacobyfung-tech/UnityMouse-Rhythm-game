using UnityEngine;
using System.Collections;

public class AnimationUtils : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public static Coroutine playOnce(
        MonoBehaviour runner,
        SpriteRenderer s,
        Sprite[] frames,
        float delay,
        float duration)
    {
        return runner.StartCoroutine(playOnceC(s, frames, delay,duration));
    }

    private static IEnumerator playOnceC(SpriteRenderer s, Sprite[] frames, float delay,float duration){
        
        float start = Time.time;
        float elapsed = 0f;
        while(true){
            foreach(Sprite frame in frames){
                yield return new WaitForSeconds(delay);
                s.sprite = frame;
                elapsed = Time.time - start;
                if(elapsed > duration){
                    yield break;
                }
                
            }
        }
    }





}
