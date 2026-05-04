using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NormalSubStatus:Status
{
    private GameSubFSM mananger;
    public NormalSubStatus(GameSubFSM manager)
    {
        this.mananger = manager;
    }
    // Start is called before the first frame update
    public void OnEnter()
    {
        EventCenter.Instance.Add(this, "GamePause", GamePause);
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {
        EventCenter.Instance.RemoveAll(this);
    }
    private void GamePause()
    {
        mananger.ChangeSubState(GameSubStatus.pause);
    }
}
public class PauseSubStatus:Status
{
    private GameSubFSM mananger;
    public PauseSubStatus(GameSubFSM manager)
    {
        this.mananger = manager;
    }
    // Start is called before the first frame update
    public void OnEnter()
    {
        EventCenter.Instance.Add(this, "GamePauseOver", GamePauseOver);
        Time.timeScale = 0;
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {
        EventCenter.Instance.OnTriggerEven("GamePauseUiClose");
        EventCenter.Instance.RemoveAll(this);
        Time.timeScale = 1;
    }
    private void GamePauseOver()
    {
        mananger.ChangeSubState(GameSubStatus.normal);
    }
}
public class ChoseSubStatus : Status
{
    private GameSubFSM mananger;
    public ChoseSubStatus(GameSubFSM manager)
    {
        this.mananger = manager;
    }
    // Start is called before the first frame update
    public void OnEnter()
    {
        EventCenter.Instance.OnTriggerEven("ChoseSkillUi");
        EventCenter.Instance.OnTriggerEven("ChoseSkill");//TODO:¼¼ÄÜÊ÷
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}
