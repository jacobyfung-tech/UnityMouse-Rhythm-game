using UnityEngine;
using System.Collections.Generic;
public class JudgeInput : MonoBehaviour
{
    public Queue<NoteInstance> line = new Queue<NoteInstance>(); 
    public HammerAnimate hammerAnimator;
    public NoteInstance note;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void PrintQueue(Queue<NoteInstance> line)
    {
        string s = "Queue (" + line.Count + "): ";
        foreach (NoteInstance n in line)
            s += n.name + ", ";

            

        Debug.Log(s);
    }
    // Update is called once per frame
    public void Judge()
    {
        if(line.Count < 1) return;
        note = line.Peek();
        if (note == null) return;    

        if(line.Count > 0 &&
            note.currentState == NoteInstance.state.HitWindowOpen)
        {
            note.missed = false;
            Debug.Log("Registered on beat input");
            checkHowOff();
        }
        else
        {
            //note.missed = false;
            note.missed = true;
            Debug.Log("Registered off beat input");
            checkHowOff();

        }
    }
    void checkHowOff()
    {
        float neutral = line.Peek().createdT + line.Peek().timeUntilHit;
        Debug.Log("Apparently off by " + (Time.timeAsDouble-neutral) + "dequeing note " + note.gameObject.name);
        line.Dequeue();
    }
    void Update()
    {
        while (line.Count > 0 &&
            (line.Peek() == null ||
            line.Peek().currentState == NoteInstance.state.HitWindowClosed))
        {
            note = line.Peek();
            //Debug.Log("Dequed " + line.Peek().gameObject.name);

            Debug.Log("dequeue " + note.gameObject.name + "because it was missed");
            line.Dequeue();   // notes out of window can be forgotten, will destroy themselves in a few seconds
        }
    }
}
