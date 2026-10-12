using UnityEngine;

public class HammerInput : MonoBehaviour
{
    public Inputs inputHandler;
    public HammerAnimate hammerAnimator;
    public float mouseThreshold; //threshold for mouse movement to trigger hammer swing, i'd recommend 0.25

    public float lastDMouseX;
    public float lastDMouseY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float DMouseX = inputHandler.GetMouseX(); //currently unused but we should implement anyway
        float DMouseY = inputHandler.GetMouseY();

        if (DMouseY > mouseThreshold && lastDMouseY <= mouseThreshold) //debounce
        {
            Debug.Log("Mouse moved up by " + DMouseY + ", swinging hammer.");
            hammerAnimator.StartSwing();
        }


        lastDMouseX = DMouseX;
        lastDMouseY = DMouseY;
    }
}
