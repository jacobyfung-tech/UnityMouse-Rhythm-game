using UnityEngine;

public class HammerInput : MonoBehaviour
{
    public Inputs inputHandler;
    public HammerAnimate hammerAnimator;
    public float mouseThreshold; //threshold for mouse movement to trigger hammer swing, i'd recommend 0.25

    public float lastDMouseX;
    public float lastDMouseY;
    public bool leftClickDown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
            hammerAnimator.StartSwing();
        }

        if (
            DMouseY < -mouseThreshold/10 &&
            lastDMouseY >= -mouseThreshold/10 &&
            leftClickDown) //debounce
        {

            Debug.Log("Mouse moved down by " + DMouseY + ", resetting hammer.");
            hammerAnimator.ResetHammer();
        }



        lastDMouseX = DMouseX;
        lastDMouseY = DMouseY;
    }
}
