using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InitStatu : Status
{
    private float timer = 0f;
    private GameManage gameManage;
    public void OnEnter()
    {
        timer = 0;
        GameManage.instance.Init();
        Debug.Log("初始化");
    }
    public void OnUpdate()
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            GameManage.instance.ChangeStatus(GameStatus.menu);
        }

    }
    public void OnExit()
    {

    }
}
public class MenuStatu : Status
{
    public void OnEnter()
    {

        Debug.Log("游戏主菜单");
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}
public class GamePlayStatu : Status
{
    private GameSubFSM subFSM = new GameSubFSM();
    public void OnEnter()
    {
        Debug.Log("游戏开始");
        EventCenter.Instance.OnTriggerEven("MonsterMangerReset");//服务内部动态数据需要重新回到初始状态
        EventCenter.Instance.OnTriggerEven("PlayerBirth");
        EventCenter.Instance.OnTriggerEven("MonsterBirth");
        EventCenter.Instance.OnTriggerEven("InputOpen");
        EventCenter.Instance.OnTriggerEven("BagClear");
        subFSM.Init();
        subFSM.ChangeSubState(GameSubStatus.normal);

    }
    public void OnUpdate()
    {

        subFSM.currentstatus.OnUpdate();
    }
    public void OnExit()
    {
        subFSM.ChangeSubState(GameSubStatus.over);
        EventCenter.Instance.OnTriggerEven("InputClose");
        EventCenter.Instance.OnTriggerEven("PushClearAll");
        EventCenter.Instance.OnTriggerEven("MonsterAllDie");
    }
}
public class GameOverStatu : Status
{
    public void OnEnter()
    {
        Debug.Log("游戏结束");
        
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}
