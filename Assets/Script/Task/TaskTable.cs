using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Task/TaskTable",fileName = "TaskTable")]
public class TaskTable : ScriptableObject
{
    private static TaskTable instance = null;
    public static TaskTable Instance
    {
        get
        {
            if(instance != null)
            {
                return instance;
            }
            instance = Resources.Load<TaskTable>("TaskTable");
            if (instance == null)
                Debug.Log("TaskTableŒ¥≈‰÷√");
            return instance;
        }
    }

    public List<TaskDate> tasks = new List<TaskDate>();

    public List<TaskDate> Get()
    {
        return tasks;
    }

}
