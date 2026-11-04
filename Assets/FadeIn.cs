using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class FadeInOut : MonoBehaviour
{
    public float intensity;
    public float error = 0.01f; //annoyingly, lerping will never get us to the target
    public float rate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator FadeIn(float pause){
        
        float orig = VectorUtils.globalWobbleMult;
        VectorUtils.globalWobbleMult = intensity;
        yield return new WaitForSeconds(pause);
        for(int i = 0; i < 200; i++)
        {
            yield return null;
            VectorUtils.globalWobbleMult = Mathf.Lerp(VectorUtils.globalWobbleMult, orig, rate);
        }
        VectorUtils.globalWobbleMult = orig;
    }
}
