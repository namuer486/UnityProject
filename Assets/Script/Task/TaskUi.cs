using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskUi : MonoBehaviour,BasePanel
{
    public TaskDate date {  get; private set; }
    public Button Start {  get; private set; }
    public Button Reward {  get; private set; }
    public TextMeshProUGUI text {  get; private set; }

    public void Init()
    {
        Start=transform.Find("TaskStart").GetComponent<Button>();
        Reward = transform.Find("GetReward").GetComponent<Button>();
        text = transform.Find("TaskText").GetComponent<TextMeshProUGUI>();
        EventCenter.Instance.Add(this, "TaskRewardOpen", TaskRewardOpen);
        EventCenter.Instance.Add(this, "TaskRewardClose", TaskRewardClose);
        EventCenter.Instance.Add(this, "TaskStartOpen", TaskStartOpen);
        EventCenter.Instance.Add(this, "TaskStartClose", TaskStartClose);
        EventCenter.Instance.Add(this, "UpdateTaskUi", UpdateTaskUi);

        Start.onClick.AddListener(TaskManager.Instance.LisTask);
        Reward.onClick.AddListener(TaskManager.Instance.RewardGet);
        Reward.gameObject.SetActive(false);
        UpdateTaskUi();
    }
    private void UpdateTaskUi()
    {
        date = TaskManager.Instance.GetTask();
        string st;
        switch (date.TaskType)
        {
            case TaskType.Live:
                st = "Live";
                break;
            case TaskType.Kill:
                st = "Kill";
                break;
            default:
                st = "None";
                break;
        }
        text.text = "Action£º" + st + " " + date.TaskTypeNum.ToString()+"\nReward: ±¦½£"+"\nLength:"+ TaskManager.Instance.TaskLength.ToString();
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void TaskRewardOpen()
    {
        Reward.gameObject.SetActive(true);
    }
    public void TaskRewardClose()
    {
        Reward.gameObject.SetActive(false);
    }
    public void TaskStartOpen()
    {
        Start.gameObject.SetActive(true);
    }
    public void TaskStartClose()
    {
        Start.gameObject.SetActive(false);
    }
}
