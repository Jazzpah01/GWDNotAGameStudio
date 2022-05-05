using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Dialogue")]
public class Dialogue : ScriptableObject
{

    public List<DLine> lines = new List<DLine>();
    public int current = -1;

    private bool isComplete;

    public DLine GetLine(int i)
    {
        return lines[i];
    }

    public void SetComplete(bool b)
    {
        isComplete = b;
    }

    public bool GetComplete()
    {
        return isComplete;
    }
}
