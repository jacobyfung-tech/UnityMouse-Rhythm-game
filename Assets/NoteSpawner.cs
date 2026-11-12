using UnityEngine;

public class NoteSpawner : MonoBehaviour
{

    public GameObject sp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public static void createNote(float timeUntilHit, float window, float offset)
    {
        GameObject sp = GameObject.FindWithTag("Spawn");
        GameObject instance = Instantiate(GameObject.FindWithTag("NoteObj"), sp.transform.position, Quaternion.identity);
        NoteInstance scr = instance.GetComponent<NoteInstance>();
        scr.enabled = true;
        scr.timeUntilHit = timeUntilHit;
        scr.window = window;
        scr.calibrationOffset = offset;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
