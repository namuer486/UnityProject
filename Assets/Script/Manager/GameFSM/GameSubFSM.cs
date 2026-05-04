using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSubFSM
{
    public Status currentstatus {  get; private set; }
    private Dictionary<GameSubStatus,Status> keyValuePairs;
    public float chosetime {  get; private set; }
   
    public void Init()
    {
        keyValuePairs=new Dictionary<GameSubStatus,Status>();

        keyValuePairs.Add(GameSubStatus.normal, new NormalSubStatus(this));
        keyValuePairs.Add(GameSubStatus.pause, new PauseSubStatus(this));
        keyValuePairs.Add(GameSubStatus.chose, new ChoseSubStatus(this));
    }

    public void ChangeSubState(GameSubStatus status)
    {
        if (currentstatus != null)
        {
            currentstatus.OnExit();
        }
        currentstatus = keyValuePairs[status];
        currentstatus.OnEnter();
    }
    public void Pause()
    {

    }
    public void Chose()
    {

    }

}
