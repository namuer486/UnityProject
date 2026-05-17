using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskType
{
    None,
    Kill,
    Live
}

[System.Serializable]
public class TaskDate
{
    public int TaskID;
    public TaskType TaskType;
    public int TaskTypeNum;
    public int[] ItemID;
}
