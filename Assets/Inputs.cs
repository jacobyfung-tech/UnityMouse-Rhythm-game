using UnityEngine;

public class Inputs : MonoBehaviour
{
    Vector2 mousePos = Input.mousePosition;
    //normalized mouse positions
    float xP; 
    float yP;

    float xPLast; 
    float yPLast;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void HandleMouse(float dX, float dY)
    {
        //Debug.Log("Mouse delta: (" + dX + ", " + dY + ")");
    }

    void CheckMouse()
    {
        mousePos = Input.mousePosition;
        xP = mousePos.x / Screen.width;
        yP = mousePos.y / Screen.height;

    }

    public float GetMouseX()
    {
        return xP;
    }
    public float GetMouseY()
    {
        return yP;
    }

    

    // Update is called once per frame
    void Update()
    {
    
        
        CheckMouse();

        //between here, check delta of mouse since xPLast didn't update
        float deltaX = xP - xPLast;
        float deltaY = yP - yPLast;

        HandleMouse(deltaX, deltaY);

        xPLast = xP;
        yPLast = yP;
    }
}
