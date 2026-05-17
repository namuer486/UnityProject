using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class TaskManager : MonoBehaviour
{
    private static TaskManager instance;
    public static TaskManager Instance
    {
        get
        {
            if(instance != null)
            {
                return instance;
            }
            GameObject obj = new GameObject("TaskManager");
            instance = obj.AddComponent<TaskManager>();
            return instance;
        }
    }
    public List<TaskDate> tasks { get; private set; }
    private TaskDate CurrentTask;
    private int TaskIdx = 0;
    public int TaskLength { get; private set; }

    public void Init()
    {
        tasks = new List<TaskDate>(TaskTable.Instance.Get());
    }
    public TaskDate GetTask()
    {
        if (tasks == null || tasks.Count <= 0)
        {
            Debug.Log("任务表空了");
            return null;
        }
        if (TaskIdx >= tasks.Count)
            return null;
        CurrentTask=tasks[TaskIdx];
        return CurrentTask;
    }
    public TaskDate GetTask(int idx)
    {
        if (tasks == null || tasks.Count <= 0)
        {
            Debug.Log("任务表空了");
            return null;
        }
        if (idx < 0 || idx >= tasks.Count)
            return null;
        CurrentTask = tasks[idx];
        return CurrentTask;
    }
    public void LisTask()
    {
        switch (CurrentTask.TaskType)
        {
            case TaskType.Live:
                EventCenter.Instance.Add(this, "LiveOnce", CheckSuccess);
                break;
            case TaskType.Kill:
                EventCenter.Instance.Add(this, "KillOnce", CheckSuccess);
                break;
            default:
                break;
        }
        EventCenter.Instance.OnTriggerEven("TaskStartClose");
    }
    private void CheckSuccess(int num)
    {
        TaskLength = num;
        if (TaskLength >= CurrentTask.TaskTypeNum)
        {
            TaskSuccess();
        }
    }
    private void CheckSuccess()
    {
        TaskLength++;
        if (TaskLength >= CurrentTask.TaskTypeNum)
        {
            TaskSuccess();
        }
    }
    private void TaskSuccess()
    {
        EventCenter.Instance.OnTriggerEven("TaskRewardOpen");
    }
    public void RewardGet()
    {
        if (CurrentTask == null)
            return;
        for (int i = 0; i < CurrentTask.ItemID.Length; i++)
        {
            ItemConfig itemConfig = ItemTable.instance.GetConfig(CurrentTask.ItemID[i]);
            BagItem item = new BagItem(itemConfig, 1);
            EventCenter.Instance.OnTriggerEven("BagAdd", item);
        }
        EventCenter.Instance.RemoveAll(this);
        TaskIdx++;
        TaskLength = 0;
        EventCenter.Instance.OnTriggerEven("TaskRewardClose");
        EventCenter.Instance.OnTriggerEven("TaskStartOpen");
        EventCenter.Instance.OnTriggerEven("UpdateTaskUi");

    }
}
