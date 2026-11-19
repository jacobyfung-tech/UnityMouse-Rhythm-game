using UnityEngine;
using System.Collections.Generic;
public class JudgeInput : MonoBehaviour
{
    public Queue<NoteInstance> line = new Queue<NoteInstance>(); 
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
    void Update()
    {
        while (line.Count > 0 &&
            (line.Peek() == null ||
            line.Peek().currentState == NoteInstance.state.HitWindowClosed))
        {
            Debug.Log("dequed " + line.Peek().gameObject.name);
            line.Dequeue();   // notes out of window can be forgotten, will destroy themselves in a few seconds
        }
    }
}
