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
        PrintQueue(line);
    }
}
